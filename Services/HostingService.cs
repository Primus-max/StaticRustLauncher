namespace StaticRustLauncher.Services;

public class HostingService : DataService<HostingCollection>
{
    public HostingService(HttpClient httpClient) : base(httpClient)
    {
    }
}
