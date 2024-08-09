using StaticRustLauncher.Services;

using System.Collections.ObjectModel;

namespace StaticRustLauncher.Views.Pages;

/// <summary>
/// Логика взаимодействия для NewsPage.xaml
/// </summary>
public partial class NewsPage : Page
{
    public ObservableCollection<NewsItem> NewsCollection { get; set; } = [];
    public NewsItem SelectedNews { get; set; } = null!;

    public NewsPage()
    {
        InitializeComponent();
        //LoadTestData();
        DataContext = this;
        Task.Run(() => LoadServers());
    }

    async Task LoadServers()
    {
        try
        {
            HttpClient httpClient = new();
            var serverService = new NewsService(httpClient);
            var news = await serverService.GetDataAsync("http://194.147.90.218/launcher/news");
        }
        catch (Exception ex)
        {
            var asdf = ex;
        }
    }


    private void News_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var newsDetailsWindow = new NewsDetailWindow(SelectedNews);
        WindowHelper.OpenWindowWithBlur(newsDetailsWindow);
    }
}
