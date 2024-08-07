namespace StaticRustLauncher.Views.Pages;

/// <summary>
/// Логика взаимодействия для SettingsPage.xaml
/// </summary>
public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();
    }

    private void Test_MiuseOver(object sender, MouseEventArgs e)
    {
        var combo = sender as ComboBox;
        if (combo == null) return;

      
    }
}
