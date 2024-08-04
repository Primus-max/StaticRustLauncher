using System.Collections.ObjectModel;

namespace StaticRustLauncher.Views.Pages;

/// <summary>
/// Логика взаимодействия для NewsPage.xaml
/// </summary>
public partial class NewsPage : Page
{
    public ObservableCollection<News> NewsCollection { get; set; } = [];
    public News SelectedNews { get; set; } = null!;

    public NewsPage()
    {
        InitializeComponent();
        LoadTestData();
        DataContext = this;
    }

    private void LoadTestData()
    {
        NewsCollection = new ObservableCollection<News>
        {
            new News
            {
                Title = "Новость 1",
                Description = "Описание первой новости",
                ReleaseDate = DateTime.Now.AddDays(-1)
            },
            new News
            {
                Title = "Новость 2",
                Description = "Описание второй новости",
                ReleaseDate = DateTime.Now.AddDays(-2)
            },
            new News
            {
                Title = "Новость 3",
                Description = "Описание третьей новости",
                ReleaseDate = DateTime.Now.AddDays(-3)
            },
            new News
            {
                Title = "Новость 4",
                Description = "Описание четвертой новости",
                ReleaseDate = DateTime.Now.AddDays(-4)
            },
            new News
            {
                Title = "Новость 5",
                Description = "Описание пятой новости",
                ReleaseDate = DateTime.Now.AddDays(-5)
            }
        };
    }


    private void News_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var newsDetailsWindow = new NewsDetailWindow(SelectedNews);
        WindowHelper.OpenWindowWithBlur(newsDetailsWindow);
    }
}
