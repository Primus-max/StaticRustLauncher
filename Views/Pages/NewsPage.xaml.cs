namespace StaticRustLauncher.Views.Pages;

/// <summary>
/// Логика взаимодействия для NewsPage.xaml
/// </summary>
public partial class NewsPage : Page
{
    public NewsPage()
    {
        InitializeComponent();
    }

    private void News_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataContext is NewsViewModal viewModel)
        {
            var selectedNews = viewModel.SelectedNews;
            if (selectedNews != null)
            {
                var newsDetailsWindow = new NewsDetailWindow(selectedNews);
                WindowHelper.OpenWindowWithBlur(newsDetailsWindow);
            }
        }
    }
}
