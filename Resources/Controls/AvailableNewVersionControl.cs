

namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для AvalibleNewVirsionControl.xaml
/// </summary>
public partial class AvailableNewVersionControl : System.Windows.Controls.UserControl
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

        string remoteFilePath = SettingsApp.GamesPath + "/" + "Actual";       
        string installPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsApp.DirGame, "Actual");

        UpdateService updateService = new();
        Thread downloadThread = new (() =>  updateService.Check(remoteFilePath, installPath));
        downloadThread.Start();
        //updateService.Check(remoteFilePath, installPath);//updateService.Check(remoteFilePath, installPath);
    }
}
