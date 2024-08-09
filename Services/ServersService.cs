
using StaticRustLauncher.Services.Base;

namespace StaticRustLauncher.Services;

public class ServerService : DataService<Server>
{
    public ServerService(HttpClient httpClient) : base(httpClient) { }
}
