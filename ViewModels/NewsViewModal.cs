namespace StaticRustLauncher.ViewModels;

public class NewsViewModal : BaseViewModel
{
    private ObservableCollection<NewsItem> _newsCollection = null!;
    private NewsItem _selectedNewsItem = null!;
    public ObservableCollection<NewsItem> NewsCollection
    {
        get => _newsCollection;
        set => Set(ref _newsCollection, value);
    }
    public NewsItem SelectedNews 
    {
        get => _selectedNewsItem;
        set => Set(ref _selectedNewsItem, value);
    }

    public NewsViewModal()
    {
        LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            HttpClient httpClient = new();
            var newsService = new NewsService(httpClient);
            var loadedNews = await newsService.GetDataAsync("http://194.147.90.218/launcher/news");

            // Присваиваем новую коллекцию
            NewsCollection = new ObservableCollection<NewsItem>(loadedNews);
        }
        catch (Exception ex)
        {
            // Обработка исключений
            var asdf = ex;
        }
    }


}
