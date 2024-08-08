namespace StaticRustLauncher.ViewModels;

public class HostsWindowViewModel
{
    public Hosting? SelectedHost { get; set; } = null!;

    public HostsWindowViewModel(Hosting selectedHost) =>
        SelectedHost = selectedHost;
}
