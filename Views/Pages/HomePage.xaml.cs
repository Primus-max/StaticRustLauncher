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
            var selectedNews = viewModel.SelectedServer;
            if (selectedNews != null)
            {
                var newsDetailsWindow = new ServerDetailWindow(selectedNews);
                WindowHelper.OpenWindowWithBlur(newsDetailsWindow);
            }
        }       
    }
}
