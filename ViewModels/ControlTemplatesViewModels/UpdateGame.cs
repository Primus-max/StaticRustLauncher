namespace StaticRustLauncher.ViewModels.ControlTemplatesViewModels;

class UpdateGame
{
    public string Version { get; set; }
    public ICommand DownloadCommand { get; set; }
}
