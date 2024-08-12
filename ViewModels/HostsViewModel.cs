namespace StaticRustLauncher.ViewModels;

class HostsViewModel : BaseViewModel
{
    private ObservableCollection<Hosting> _hosts = null!;
    private Hosting _selectedHosting = null!;
    public ObservableCollection<Hosting> Hosts
    {
        get => _hosts;
        set => Set(ref _hosts, value);
    }
    public Hosting SelectedHosting
    {
        get => _selectedHosting;
        set => Set(ref _selectedHosting, value);
    }

    public HostsViewModel()
    {
        Task.Run(() => LoadHostsAsync());
    }


    async Task LoadHostsAsync()
    {
        try
        {
            using HttpClient httpClient = new();
            var hostingService = new HostingService(httpClient);
            var hosts = await hostingService.GetDataAsync("http://194.147.90.218/launcher/hostings");
            Hosts = new ObservableCollection<Hosting>(hosts.OrderByDescending(host => host.Status == 0));
        }
        catch (Exception)
        {
            // Логирование
        }
    }

}
