using StaticRustLauncher.EventHandlers;

namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для AvalibleNewVirsionControl.xaml
/// </summary>
public partial class AvailableNewVersionControl : UserControl
{

    public event Action DownloadStarted;

    public AvailableNewVersionControl()
    {
        InitializeComponent();
        LoadVersionAsync();
    }

    private async void LoadVersionAsync()
    {
        var result = await CheckVersion();        
        Dispatcher.Invoke(() =>
        {
            NewVersionText.Text = result;
        });
    }

    public async Task<string?> CheckVersion()
    {
        var actualVersionTulip = await UpdateCheckerService.IsUpdateAvailableAsync();
        return actualVersionTulip.Item2 as string; 
    }

    public void DownloadNewVersion_Click(object sender, EventArgs e)
    {
        EventBus.OnDownloadStarted();

        string host = "sftp.hoster.by";
        int port = 22;
        string username = "user2120";
        string password = "uT3cB1xE1r";

        string remoteFilePath = "/storage/user2120/data/Actual";       
        string installPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsApp.DirGame);

        UpdateService updateService = new(host, port, username, password);
        Thread downloadThread = new (() =>  updateService.Check(remoteFilePath, installPath));
        downloadThread.Start();
        //updateService.Check(remoteFilePath, installPath);
    }
}
