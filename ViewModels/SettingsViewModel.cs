
using Microsoft.Win32;

namespace StaticRustLauncher.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    public ICommand? OpenFileDialogCommand { get; }

    private string? _launcherFilePath;
    private string? _gameFilePath;
    public string? LauncherFilePath 
    { 
        get => _launcherFilePath; 
        set => Set(ref _launcherFilePath, value); 
    }
    public string? GameFilePath 
    { 
        get => _gameFilePath; 
        set => Set(ref _gameFilePath, value); 
    }

    public SettingsViewModel()
    {
        OpenFileDialogCommand = new LambdaCommand(OnOpenFileDialog);        
    }

    private void OnOpenFileDialog(object commandName)
    {
        OpenFileDialog openFile = new();
        if (openFile is null || openFile.ShowDialog() != true) return;

        switch (commandName)
        {
            case "Launcher":
                LauncherFilePath = openFile.FileName;
                break;
            case "Game":
                GameFilePath = openFile.FileName;
                break;
            default:
                break;
        }

    }

}
