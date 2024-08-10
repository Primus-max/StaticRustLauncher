namespace StaticRustLauncher.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private ObservableCollection<Server> _servers = null!;
    private Server _selectedServer = null!;
    private string _version = string.Empty;
    public ObservableCollection<Server> ServersCollection
    {
        get => _servers;
        set => Set(ref _servers, value);
    }
    public Server SelectedServer
    {
        get => _selectedServer;
        set => Set(ref _selectedServer, value);
    }

    public string Version
    {
        get => _version;
        set => Set(ref _version, value);
    }

    public HomeViewModel()
    {
        Task.Run(() => LoadServersAsync());
    }

    async Task LoadServersAsync()
    {
        try
        {
            using HttpClient httpClient = new();
            var serverService = new ServerService(httpClient);
            var servers = await serverService.GetDataAsync("http://194.147.90.218/launcher/serversinfo");
            ServersCollection = new ObservableCollection<Server>(servers);
        }
        catch (Exception ex)
        {
            var asdf = ex;
        }
    }

}
