namespace StaticRustLauncher.ViewModels;

public class ServerDetailViewModel
{

    public Server SelectedServer =   null!;

    public ServerDetailViewModel(Server selectedServer = null!) =>   
        SelectedServer = selectedServer;
   


}
