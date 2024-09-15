namespace StaticRustLauncher.ViewModels;

public class NewsDetailWindowViewModel : BaseViewModel
{
    private NewsItem? _selectedNews = null!;
    public NewsItem? SelectedNews
    {
        get => _selectedNews;
        set => Set(ref _selectedNews, value);
    }

    public NewsDetailWindowViewModel(NewsItem selectedNews) =>    
        SelectedNews = selectedNews;
}
