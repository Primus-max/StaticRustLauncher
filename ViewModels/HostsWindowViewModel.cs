namespace StaticRustLauncher.ViewModels;

public class HostsWindowViewModel
{
    public Host? SelectedHost { get; set; } = null!;

    public HostsWindowViewModel(Host selectedHost) =>
        SelectedHost = selectedHost;
}
