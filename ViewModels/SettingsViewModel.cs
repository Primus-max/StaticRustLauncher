using Newtonsoft.Json;

namespace StaticRustLauncher.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    public ICommand? OpenFileDialogCommand { get; }
    public ICommand? SaveSettingsCommand { get; }
    public ICommand? CheckAllHashVersionGames { get; }

    private string? _selectedLang = null!;
    private string? _gameVersion = null!;
    private string? _dirLauncher = null!;
    private string? _dirGame = null!;

    public ObservableCollection<string> Languages => Models.Languages.LanguageList;
    public ObservableCollection<string> GameVersions => Task.Run(async () => await GetListVersionsAsync()).Result;

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
        CheckAllHashVersionGames = new LambdaCommand(OnCheckAllHashVersionGemas);

        SelectedLang = SettingsApp.Lang;
        GameVersion = SettingsApp.GameVersion;
        DirLauncher = SettingsApp.DirLauncher;
        DirGame = string.IsNullOrEmpty(SettingsApp.DirGame)
            ? AppDomain.CurrentDomain.BaseDirectory + "game"
            : SettingsApp.DirGame;
    }

    private void OnSaveSettings(object commandName)
    {
        SettingsApp.Lang = SelectedLang ?? string.Empty;
        SettingsApp.GameVersion = GameVersion ?? string.Empty;
        SettingsApp.DirLauncher = DirLauncher ?? string.Empty;
        SettingsApp.DirGame = string.IsNullOrEmpty(DirGame)
            ? AppDomain.CurrentDomain.BaseDirectory + "game"
            : DirGame;

        SettingsApp.Save();
    }

    private void OnOpenFileDialog(object commandName)
    {
        using var folderDialog = new FolderBrowserDialog();
        folderDialog.Description = "Выберите папку"; // Описание в диалоге
        var result = folderDialog.ShowDialog();

        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
        {
            switch (commandName)
            {
                case "Launcher":
                    DirLauncher = folderDialog.SelectedPath;
                    break;
                case "Game":
                    DirGame = folderDialog.SelectedPath;
                    break;
                default:
                    break;
            }
        }
    }

    private async Task<ObservableCollection<string>> GetListVersionsAsync()
    {
        var data = await GetDataInfo();

        // Получаем данные из секции "clients"
        if (data != null && data.TryGetValue("clients", out var clientsData))
        {
            var clients = JsonConvert.DeserializeObject<Dictionary<string, string>>(clientsData.ToString());

            var devVersions = clients?
                .Values
                .ToList() ?? [];

            return new ObservableCollection<string>(devVersions);
        }

        return [];
    }

    private async Task<Dictionary<string, object>> GetDataInfo()
    {
        const string apiUrl = "http://194.147.90.218/launcher/version";
        using var httpClient = new HttpClient();

        try
        {
            var response = await httpClient.GetStringAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            return data ?? [];
        }
        catch (Exception ex)
        {
            // Обработка ошибок
            Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            return [];
        }
    }

    public async Task CheckSelectedVersionGame()
    {
        string pathToSelectedVersion = string.Empty;
        string destLocalPath = String.Empty;
        string? selectedGameVersionFolder = string.Empty;

        string remoteDirectory = SettingsApp.GamesPath;
        using var sftpClient = await SFTPClient.GetAsync();
        sftpClient.Connect();

        var data = await GetDataInfo();
        
        if (data != null && data.TryGetValue("clients", out var clientsData))
        {
            var clients = JsonConvert.DeserializeObject<Dictionary<string, string>>(clientsData?.ToString());
            selectedGameVersionFolder = clients?.FirstOrDefault(x => x.Value == GameVersion).Key;
        }
        
        var selectedVersionDirectory = GetRemoteDirectories(sftpClient, remoteDirectory)
            .Where(path => path.Name.Contains(selectedGameVersionFolder, StringComparison.OrdinalIgnoreCase))
            .Select(path => path.Name)
            .SingleOrDefault();

        sftpClient.Disconnect();

        pathToSelectedVersion = remoteDirectory + "/" + selectedVersionDirectory;
        destLocalPath = SettingsApp.DirGame + "\\" + selectedVersionDirectory;

        UpdateService updateService = new();
        SettingsApp.GameVersion = GameVersion;
        SettingsApp.Save();
        Thread thread = new(() => updateService.Check(pathToSelectedVersion, destLocalPath, GameVersion));
        EventBus.OnDownloadStarted();
        thread.Start();
    }

    private List<ISftpFile> GetRemoteDirectories(SftpClient sftpClient, string remoteDirectory)
    {
        var remoteDirectories = sftpClient.ListDirectory(remoteDirectory)
            .Where(entry => entry.IsDirectory &&
                            entry.Name.Contains("Dev", StringComparison.OrdinalIgnoreCase) ||
                            entry.Name.Contains("Actual", StringComparison.OrdinalIgnoreCase))
            .ToList();

        return remoteDirectories;
    }

    private string[] GetOldVersionsList(IEnumerable<string> devDirectories)
    {
        return devDirectories
            .Select(name => Regex.Match(name, @"\d+").Value)
            .Where(version => !string.IsNullOrEmpty(version))
            .ToArray();
    }

    private void OnCheckAllHashVersionGemas(object commandName)
    {
        EventBus.OnDownloadStarted();
        Task.Run(async () =>
        {
            var allDirectoriesDev = Directory.GetDirectories(SettingsApp.DirGame)
                .Where(dir => dir.Contains("dev", StringComparison.OrdinalIgnoreCase))
                .ToList();

            SftpClient sftpClient = await SFTPClient.GetAsync();
            try
            {
                sftpClient.Connect();

                var allRemoteDevDirectories = GetRemoteDirectories(sftpClient, SettingsApp.GamesPath)
                    .Select(path => path.FullName)
                    .ToList();

                UpdateService updateService = new();

                foreach (var dir in allRemoteDevDirectories)
                {
                    var devFolderName = dir.Split("/").Last();

                    var localDevDirectory = allDirectoriesDev
                        .FirstOrDefault(
                            localDir => localDir.Contains(devFolderName, StringComparison.OrdinalIgnoreCase));

                    if (string.IsNullOrEmpty(localDevDirectory))
                        localDevDirectory = Path.Combine(SettingsApp.DirGame, devFolderName);

                    Console.WriteLine($"Remote directory: {dir}, Local directory: {localDevDirectory}");

                    updateService.Check(dir, localDevDirectory);
                    SettingsApp.AddGameVersion(GameVersion, localDevDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (sftpClient.IsConnected)
                {
                    sftpClient.Disconnect();
                }
            }
        });
    }
}