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
    private bool _isCancellationRequested = false;
    private List<ISftpFile> _files = [];

    public UpdateService(string host, int port, string username, string password)
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;

        EventBus.CancelDownloading += OnCancelDownloading;
    }

    private void OnCancelDownloading()
    {
        _isCancellationRequested = true;
        EventBus.OnDownloadCompleted();
    }

    public void Check(string remoteDirectoryPath, string destLocalPath)
    {
        try
        {
            using var sftpClient = new SftpClient(_host, _port, _username, _password);
            sftpClient.Connect();

            // Загрузка и парсинг hashList.txt
            var hashList = GetHashList(sftpClient, remoteDirectoryPath);

            // Преобразование IEnumerable в List для использования индекса
             _files = sftpClient.ListDirectory(remoteDirectoryPath).ToList();
            _totalFileSize = CalculateTotalSize(_files, sftpClient);
            // Скачивание файлов с проверкой хешей
            DownloadDirectory(sftpClient, remoteDirectoryPath, destLocalPath, hashList);

            sftpClient.Disconnect();

            Dispatcher.CurrentDispatcher.Invoke(new Action(() => EventBus.OnDownloadCompleted()));
            
            string message = _isCancellationRequested
                ? "Загрузка обновления отменена пользователем"
                : "Загрузка завершена, наслаждайтесь игрой.";

            MessageBox.Show(message);
            _isCancellationRequested = false;
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
            while ((line = reader.ReadLine()) != null && !_isCancellationRequested)
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
        try
        {
            // Очередь директорий для обработки
            var directoriesQueue = new Queue<(string remoteDir, string localDir)>();
            directoriesQueue.Enqueue((remoteDirectoryPath, destLocalPath));

            while (directoriesQueue.Count > 0)
            {
                var (currentRemoteDir, currentLocalDir) = directoriesQueue.Dequeue();
                var files = sftpClient.ListDirectory(currentRemoteDir);

                foreach (var file in files)
                {
                    if (_isCancellationRequested)
                        return;

                    if (file.Name == "." || file.Name == "..")
                        continue;

                    // Определяем локальный и относительный путь
                    string relativePath = Path.GetRelativePath(remoteDirectoryPath, Path.Combine(currentRemoteDir, file.Name));
                    string localPath = Path.Combine(destLocalPath, relativePath);
                    Console.WriteLine(localPath);
                    if (file.IsDirectory)
                    {
                        // Создаем локальную директорию, если ее нет
                        if (!Directory.Exists(localPath))
                        {
                            Directory.CreateDirectory(localPath);
                        }

                        // Добавляем поддиректорию в очередь для дальнейшей обработки
                        directoriesQueue.Enqueue((file.FullName, localPath));
                    }
                    else if (file.IsRegularFile)
                    {
                        if (!hashList.TryGetValue(relativePath, out string remoteFileHash))
                        {
                            // Скачиваем файл, если его хеш не найден                        
                            DownloadFile(sftpClient, file.FullName, localPath, _totalFileSize);
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
                                    if (_isCancellationRequested)
                                        return;

                                    attempt++;

                                    try
                                    {
                                        DownloadFile(sftpClient, file.FullName, localPath, _totalFileSize);
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
        }
        catch (Exception)
        {
            Check(remoteDirectoryPath, destLocalPath);
            _tryCountRetryDownload++;
            if (_tryCountRetryDownload == _maxRetryAttempts)
                throw;
        }
    }


    private void DownloadFile(SftpClient sftpClient, string remoteFilePath, string localFilePath, long totalFileSize)
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
                if (_isCancellationRequested)
                    return;

                fileStream.Write(buffer, 0, bytesRead);
                fileBytesRead += bytesRead; // Увеличиваем счетчик текущего файла
                _totalBytesDownloaded += bytesRead; // Увеличиваем общий счетчик загруженных байтов

                double overallProgress = (double)_totalBytesDownloaded / totalFileSize * 100.0;
                double roundedProgress = Math.Round(overallProgress, 2);

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
            try
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
            catch (Exception)
            {
                continue;
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

