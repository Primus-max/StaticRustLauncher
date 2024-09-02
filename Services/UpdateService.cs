using Renci.SshNet;
using Renci.SshNet.Sftp;

using StaticRustLauncher.EventHandlers;

using System.Security.Cryptography;
using System.Windows.Threading;

namespace StaticRustLauncher.Services;

internal class UpdateService
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;
    private const int _maxRetryAttempts = 5; // Кол-во попыток скачивания
    long _totalFileSize = 0;// Подсчитываем общий размер файлов
    long _totalBytesDownloaded = 0; // Общее количество скачанных байтов
    int _tryCountRetryDownload = 0;

    public UpdateService(string host, int port, string username, string password)
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;
    }

    public void Check(string remoteDirectoryPath, string destLocalPath)
    {
        try
        {
            using var sftpClient = new SftpClient(_host, _port, _username, _password);
            sftpClient.Connect();

            // Загрузка и парсинг hashList.txt
            var hashList = GetHashList(sftpClient, remoteDirectoryPath);

            // Скачивание файлов с проверкой хешей
            DownloadDirectory(sftpClient, remoteDirectoryPath, destLocalPath, hashList);

            sftpClient.Disconnect();

            //MessageBox.Show("Загрузка завершена, наслаждайтесь игрой.");
            Dispatcher.CurrentDispatcher.Invoke(new Action(() => EventBus.OnDownloadCompleted()));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось скачать файлы, попробуйте запустить ещё раз: {ex.Message}");
        }
    }

    private Dictionary<string, string> GetHashList(SftpClient sftpClient, string remoteDirectoryPath)
    {
        var hashList = new Dictionary<string, string>();
        string remoteHashListPath = Path.Combine(remoteDirectoryPath, "hashList.txt").Replace('\\', '/');

        using (var remoteFileStream = sftpClient.OpenRead(remoteHashListPath))
        using (var reader = new StreamReader(remoteFileStream))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    var filePath = parts[0];
                    string fileHash = parts[1];
                    hashList[filePath] = fileHash;
                }
            }
        }

        return hashList;
    }

    private void DownloadDirectory(SftpClient sftpClient, string remoteDirectoryPath, string destLocalPath, Dictionary<string, string> hashList)
    {
        // Преобразование IEnumerable в List для использования индекса
        List<ISftpFile> files = sftpClient.ListDirectory(remoteDirectoryPath).ToList();
        _totalFileSize = CalculateTotalSize(files, sftpClient);

        try
        {
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file.Name == "." || file.Name == "..")
                    continue;

                string localPath = Path.Combine(destLocalPath, file.Name);
                string relativePath = localPath.Replace(SettingsApp.DirGame, "").TrimStart(Path.DirectorySeparatorChar);


                if (file.IsDirectory)
                {
                    Directory.CreateDirectory(localPath);
                    DownloadDirectory(sftpClient, file.FullName, localPath, hashList);
                }
                else if (file.IsRegularFile)
                {
                    if (!hashList.TryGetValue(relativePath, out string remoteFileHash))
                    {
                        // Скачиваем файл, если его хеш не найден                        
                        DownloadFile(sftpClient, file.FullName, localPath, _totalFileSize, ref _totalBytesDownloaded);
                    }
                    else
                    {
                        bool fileExistsAndValid = File.Exists(localPath) && ComputeFileHash(localPath) == remoteFileHash;
                        Console.WriteLine($"Hash файла на сервере {remoteFileHash}");
                        if (!fileExistsAndValid)
                        {
                            bool isFileDownloaded = false;
                            int attempt = 0;

                            while (!isFileDownloaded && attempt < _maxRetryAttempts)
                            {
                                attempt++;

                                try
                                {
                                    DownloadFile(sftpClient, file.FullName, localPath, _totalFileSize, ref _totalBytesDownloaded);
                                    isFileDownloaded = ComputeFileHash(localPath) == remoteFileHash;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Ошибка при скачивании файла {file.Name}: {ex.Message}");
                                }
                            }

                            if (!isFileDownloaded)
                            {
                                Console.WriteLine($"Не удалось корректно скачать файл {file.Name} после {attempt} попыток.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            Check(remoteDirectoryPath, destLocalPath);
            _tryCountRetryDownload++;
            if (_tryCountRetryDownload == _maxRetryAttempts)
                throw;
        }
    }


    private void DownloadFile(SftpClient sftpClient, string remoteFilePath, string localFilePath, long totalFileSize, ref long totalBytesDownloaded)
    {
        try
        {
            using var fileStream = File.Create(localFilePath); // Файл для записи
            using var remoteFileStream = sftpClient.OpenRead(remoteFilePath); // Удаленный файл для чтения
            var buffer = new byte[8192];
            int bytesRead;
            long fileSize = remoteFileStream.Length;
            long fileBytesRead = 0; // Отслеживание байтов, загруженных для текущего файла

            Console.WriteLine($"Скачиваю файл {remoteFilePath} в {localFilePath}");

            while ((bytesRead = remoteFileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, bytesRead);
                fileBytesRead += bytesRead; // Увеличиваем счетчик текущего файла
                totalBytesDownloaded += bytesRead; // Увеличиваем общий счетчик загруженных байтов
                                
                double overallProgress = (double)totalBytesDownloaded / totalFileSize * 100;
                int roundedProgress = (int)Math.Round(overallProgress); 
                                
                EventBus.NotifyDownloadProgressChanged(roundedProgress);
            }

            Console.WriteLine($"Завершено скачивание файла {remoteFilePath}. Размер: {fileSize} байт.");
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            Console.WriteLine($"Произошла ошибка при скачивании файла {remoteFilePath}: {ex.Message}");
            MessageBox.Show($"Произошла не запланированная ошибка {ex.Message}");
            throw;
        }
    }



    private long CalculateTotalSize(List<ISftpFile> files, SftpClient sftpClient)
    {
        long totalSize = 0;
        foreach (var file in files)
        {
            if (file.IsRegularFile)
            {
                totalSize += file.Attributes.Size;
            }
            else if (file.IsDirectory && file.Name != "." && file.Name != "..")
            {
                totalSize += CalculateTotalSize(sftpClient.ListDirectory(file.FullName).ToList(), sftpClient);
            }
        }
        return totalSize;
    }



    private string ComputeFileHash(string filePath)
    {        
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(filePath);
        byte[] fileMD5 = md5.ComputeHash(stream);
        var hash = UrlTokenEncode(fileMD5);
        Console.WriteLine($"Hash {hash} файл {filePath} ");
        return hash;
    }

    private static string UrlTokenEncode(byte[] input)
    {
        if (input == null || input.Length < 1)
            return string.Empty;

        string base64Str = Convert.ToBase64String(input);
        if (base64Str == null)
            return null!;

        int endPos;
        for (endPos = base64Str.Length; endPos > 0; endPos--)
        {
            if (base64Str[endPos - 1] != '=')
            {
                break;
            }
        }

        char[] base64Chars = new char[endPos + 1];
        base64Chars[endPos] = (char)((int)'0' + base64Str.Length - endPos);

        for (int iter = 0; iter < endPos; iter++)
        {
            char c = base64Str[iter];

            switch (c)
            {
                case '+':
                    base64Chars[iter] = '-';
                    break;

                case '/':
                    base64Chars[iter] = '_';
                    break;

                default:
                    base64Chars[iter] = c;
                    break;
            }
        }

        return new string(base64Chars);
    }
}

