namespace StaticRustLauncher.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private ObservableCollection<Server> _servers = new ObservableCollection<Server>();
    private Server _selectedServer;
    private string _version = string.Empty;
    private int _usersOnline;
    private int _serversOnline;

    public ObservableCollection<Server> ServersCollection
    {
        get => _servers;
        set
        {
            if (Set(ref _servers, value))
            {
                _servers.CollectionChanged += (s, e) => UpdateOnlineCounts();
                UpdateOnlineCounts();
            }
        }
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

    public int UsersOnline
    {
        get => _usersOnline;
        private set => Set(ref _usersOnline, value);
    }

    public int ServersOnline
    {
        get => _serversOnline;
        private set => Set(ref _serversOnline, value);
    }

    public HomeViewModel() =>
        Task.Run(() => LoadServersAsync());


    private async Task LoadServersAsync()
    {
        try
        {
            using HttpClient httpClient = new();
            var serverService = new ServerService(httpClient);
            var servers = await serverService.GetDataAsync("http://194.147.90.218/launcher/serversinfo");

            ServersCollection = new ObservableCollection<Server>(
                servers.OrderByDescending(server => server.Status == "premium")
                .ThenByDescending(server => server.Status == "vip")
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private void UpdateOnlineCounts()
    {
        ServersOnline = GetServersOnline();
        UsersOnline = GetUsersOnline();
    }

    private int GetServersOnline() => ServersCollection.Count;
    private int GetUsersOnline() => ServersCollection.Sum(u => u.Players);
}
