namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для AvalibleNewVirsionControl.xaml
/// </summary>
public partial class AvailableNewVersionControl : UserControl
{   

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
        MessageBox.Show("Скачивание новой версии началось");
    }
}
