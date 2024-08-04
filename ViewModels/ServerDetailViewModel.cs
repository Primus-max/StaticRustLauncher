namespace StaticRustLauncher.ViewModels;

public class ServerDetailViewModel
{
    public ICommand ConnectServerCommand { get; set; } = null!;

    public Server SelectedServer { get; set; } = null!;

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
