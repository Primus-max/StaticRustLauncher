namespace StaticRustLauncher.Services;

public class NewsService : DataService<NewsCollection>
{
    public NewsService(HttpClient httpClient) : base(httpClient)
    {
    }
}
