namespace StaticRustLauncher.Views.Pages;

/// <summary>
/// Логика взаимодействия для HomePage.xaml
/// </summary>
public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void OpenDescriptionServer_Click(object sender, SelectionChangedEventArgs e)
    {
        if (DataContext is HomeViewModel viewModel)
        {
            var selectedServer = viewModel.SelectedServer;

            if (selectedServer != null)
            {
                var serverDetailsWindow = new ServerDetailWindow(selectedServer);
                WindowHelper.OpenWindowWithBlur(serverDetailsWindow);

                // Обнуление выбранного элемента
                ServersList.SelectedItem = null;
            }
        }
    }
}
