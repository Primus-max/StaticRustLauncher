namespace StaticRustLauncher.ViewModels;

public class NewsDetailWindowViewModel
{
    public NewsItem? SelectedNews { get; set; }

    public NewsDetailWindowViewModel(NewsItem selectedNews) =>    
        SelectedNews = selectedNews;
}
