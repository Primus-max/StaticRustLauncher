namespace StaticRustLauncher.Transport;

public static class SFTPClient
{
    public static SftpClient Get()
    {
        string host = SettingsApp.Host;
        int port = SettingsApp.Port;
        string username = SettingsApp.User;
        string password = SettingsApp.Pass;
        string remoteDirectory = SettingsApp.GamesPath;
               
        return new SftpClient(host, port, username, password);
    }
}
