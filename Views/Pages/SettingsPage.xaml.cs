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

}
