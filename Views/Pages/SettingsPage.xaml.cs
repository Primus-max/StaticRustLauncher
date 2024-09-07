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

    private void SelectedVersionGame(object sender, SelectionChangedEventArgs e)
    {
        var viewModel = (SettingsViewModel)DataContext;
        viewModel.CheckSelectedVersionGame();
    }
}
