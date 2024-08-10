namespace StaticRustLauncher.ViewModels;

public class HostsWindowViewModel: BaseViewModel
{
    private Hosting? _selectedHost = null!;
    public Hosting? SelectedHost 
    {
        get => _selectedHost;
        set => Set(ref _selectedHost, value);
    }

    public HostsWindowViewModel(Hosting selectedHost) =>
        SelectedHost = selectedHost;


}
