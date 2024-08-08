
namespace StaticRustLauncher.Services;

public class ServersService : DataService<ServerCollection>
{
    public ServersService(HttpClient httpClient) : base(httpClient)
    {
    }
}
