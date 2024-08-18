using Renci.SshNet;

namespace StaticRustLauncher.Services;

internal class UpdateService
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;

    public UpdateService(string host, int port, string username, string password)
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;
    }

    public void DownloadAndInstallUpdate(string remoteFilePath, string localFilePath, string installPath)
    {
        try
        {
            // Скачивание файла через SFTP
            using (var sftpClient = new SftpClient(_host, _port, _username, _password))
            {
                sftpClient.Connect();
                using (var fileStream = File.OpenWrite(localFilePath))
                {
                    sftpClient.DownloadFile(remoteFilePath, fileStream);
                }
                sftpClient.Disconnect();
            }

            // Установка обновления
            InstallUpdate(localFilePath, installPath);
        }
        catch (Exception ex)
        {
            // Обработка ошибок
            Console.WriteLine($"Ошибка при скачивании или установке обновления: {ex.Message}");
        }
    }

    private void InstallUpdate(string localFilePath, string installPath)
    {
        try
        {
         
            System.IO.Compression.ZipFile.ExtractToDirectory(localFilePath, installPath);        

            MessageBox.Show("Обновление успешно установлено.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при установке обновления: {ex.Message}");
        }
    }
}
