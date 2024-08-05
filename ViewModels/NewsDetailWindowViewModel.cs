namespace StaticRustLauncher.ViewModels;

public class NewsDetailWindowViewModel
{
    public News? SelectedNews { get; set; }

    public NewsDetailWindowViewModel(News selectedNews) =>    
        SelectedNews = selectedNews;
}
