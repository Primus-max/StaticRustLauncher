using StaticRustLauncher.Services.Base;

namespace StaticRustLauncher.Services;

public class HostingService : DataService<Hosting>
{
    public HostingService(HttpClient httpClient) : base(httpClient) { }
}

