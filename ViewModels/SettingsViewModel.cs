
using Microsoft.Win32;

namespace StaticRustLauncher.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    public ICommand? OpenFileDialogCommand { get; }
    public ICommand? SaveSettingsCommand { get; }

    private string? _selectedLang = null!;
    private string? _gameVersion = null!;
    private string? _dirLauncher = null!;
    private string? _dirGame = null!;

    public string? SelectedLang
    {
        get => _selectedLang;
        set => Set(ref _selectedLang, value);
    }
    public string? GameVersion
    {
        get => _gameVersion;
        set => Set(ref _gameVersion, value);
    }
    public string? DirLauncher
    {
        get => _dirLauncher;
        set => Set(ref _dirLauncher, value);
    }
    public string? DirGame
    {
        get => _dirGame;
        set => Set(ref _dirGame, value);
    }

    public SettingsViewModel()
    {
        OpenFileDialogCommand = new LambdaCommand(OnOpenFileDialog);
        SaveSettingsCommand = new LambdaCommand(OnSaveSettings);

        SettingsApp.Load();
        SelectedLang = SettingsApp.Lang;
        GameVersion = SettingsApp.GameVersion;
        DirLauncher = SettingsApp.DirLauncher;
        DirGame = SettingsApp.DirGame;
    }

    private void OnSaveSettings(object commandName)
    {
        SettingsApp.Lang = SelectedLang ?? string.Empty;
        SettingsApp.GameVersion = GameVersion ?? string.Empty;
        SettingsApp.DirLauncher = DirLauncher ?? string.Empty;
        SettingsApp.DirGame = DirGame ?? string.Empty;

        SettingsApp.Save();
    }

    private void OnOpenFileDialog(object commandName)
    {
        OpenFileDialog openFile = new();
        if (openFile is null || openFile.ShowDialog() != true) return;

        switch (commandName)
        {
            case "Launcher":
                DirLauncher = openFile.FileName;
                break;
            case "Game":
                DirGame = openFile.FileName;
                break;
            default:
                break;
        }

    }
}
