namespace StaticRustLauncher.ViewModels;

public class ServerDetailViewModel : BaseViewModel
{
    public ICommand ConnectServerCommand { get; set; } = null!;

    private ObservableCollection<URL> _uRLs = null!;
    public ObservableCollection<URL> URLs 
    { 
        get => _uRLs; 
        set => Set(ref _uRLs, value); 
    }

    private Server _selectedServer = null!;
    public Server SelectedServer
    {
        get => _selectedServer;
        set
        {
            if (Set(ref _selectedServer, value))
            {                
                URLs = new ObservableCollection<URL>(SelectedServer.URL ?? new List<URL>());
            }
        }
    }

    public ServerDetailViewModel(Server selectedServer = null!)
    {
        ConnectServerCommand = new LambdaCommand(OnConnectServer);
        SelectedServer = selectedServer;       
    }
            
    private void OnConnectServer(object parameter)
    {
        if (parameter is Window window)
            window.Close();
    }
}
