namespace StaticRustLauncher.Services;
public class NewsService : DataService<NewsItem>
{
    public NewsService(HttpClient httpClient) : base(httpClient) { }
}

