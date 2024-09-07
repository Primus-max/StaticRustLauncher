
using Newtonsoft.Json;

namespace StaticRustLauncher.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    public ICommand? OpenFileDialogCommand { get; }
    public ICommand? SaveSettingsCommand { get; }
    public ICommand? CheckAllHashVersionGames {  get; }

    private string? _selectedLang = null!;
    private string? _gameVersion = null!;
    private string? _dirLauncher = null!;
    private string? _dirGame = null!;
    private bool _isFirstInitialize = true;

    public ObservableCollection<string> Languages => Models.Languages.LanguageList;
    public ObservableCollection<string> GameVersions => Task.Run(async () => await GetDevVersionsAsync()).Result;

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
        DirGame = string.IsNullOrEmpty(DirGame)
            ? AppDomain.CurrentDomain.BaseDirectory + "game"
            : DirGame;
    }

    private void OnSaveSettings(object commandName)
    {
        SettingsApp.Lang = SelectedLang ?? string.Empty;
        SettingsApp.GameVersion = GameVersion ?? string.Empty;
        SettingsApp.DirLauncher = DirLauncher ?? string.Empty;
        SettingsApp.DirGame = string.IsNullOrEmpty(DirGame)
            ? AppDomain.CurrentDomain.BaseDirectory  + "game"
            : DirGame;

        SettingsApp.Save();
    }

    private void OnOpenFileDialog(object commandName)
    {
        using var folderDialog = new FolderBrowserDialog();
        folderDialog.Description = "Выберите папку"; // Описание в диалоге
        DialogResult result = folderDialog.ShowDialog();

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

    private async Task<ObservableCollection<string>> GetDevVersionsAsync()
    {
        string apiUrl = "http://194.147.90.218/launcher/version";
        using var httpClient = new HttpClient();

        try
        {
            var response = await httpClient.GetStringAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            // Получаем данные из секции "clients"
            if (data.TryGetValue("clients", out var clientsData))
            {
                var clients = JsonConvert.DeserializeObject<Dictionary<string, string>>(clientsData.ToString());

                // Фильтрация и отделение "DEV"
                var devVersions = clients
                    .Where(entry => entry.Key.StartsWith("DEV", StringComparison.OrdinalIgnoreCase))
                    .Select(entry => entry.Key.Replace("DEV", string.Empty))
                    .ToList();

                return new ObservableCollection<string>(devVersions);
            }

            return new ObservableCollection<string>();
        }
        catch (Exception ex)
        {
            // Обработка ошибок
            Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            return new ObservableCollection<string>();
        }
    }


    public async Task CheckSelectedVersionGame()
    {
        //if (_isFirstInitialize) return;

        //var existsVersionsGame = SettingsApp.OldVersions.Keys.Contains(GameVersion);
        //if (existsVersionsGame) return;

        string remoteDirectory = SettingsApp.GamesPath;
        using var sftpClient = await SFTPClient.GetAsync();
        sftpClient.Connect();

        var selectedVersionDirectory = GetDevDirectories(sftpClient, remoteDirectory)
         .Where(path => path.Name.Contains(GameVersion, StringComparison.OrdinalIgnoreCase))
         .Select(path => path.Name)
         .SingleOrDefault();

        sftpClient.Disconnect();        

        string pathToSelectedVersion = remoteDirectory + "/" + selectedVersionDirectory;
        string destLocalPath = SettingsApp.DirGame + "\\" + selectedVersionDirectory;

        UpdateService updateService = new();
        SettingsApp.GameVersion = GameVersion;
        SettingsApp.Save();
        Thread thread = new(() => updateService.Check(pathToSelectedVersion, destLocalPath, GameVersion));
        EventBus.OnDownloadStarted();       
        thread.Start();
        //_isFirstInitialize = false;
    }

    private List<ISftpFile> GetDevDirectories(SftpClient sftpClient, string remoteDirectory)
    {
        return sftpClient.ListDirectory(remoteDirectory)
           .Where(entry => entry.IsDirectory && entry.Name.Contains("dev", StringComparison.OrdinalIgnoreCase))
           .ToList();
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
        Task.Run( async() => 
        {
            var allDirectoriesDev = Directory.GetDirectories(SettingsApp.DirGame)
                .Where(dir => dir.Contains("dev", StringComparison.OrdinalIgnoreCase))
                .ToList();

            SftpClient sftpClient = await SFTPClient.GetAsync();
            try
            {
                sftpClient.Connect();

                var allRemoteDevDirectories = GetDevDirectories(sftpClient, SettingsApp.GamesPath)
                    .Select(path => path.FullName)
                    .ToList();

                UpdateService updateService = new();               

                foreach (var dir in allRemoteDevDirectories)
                {
                    var devFolderName = dir.Split("/").Last();

                    var localDevDirectory = allDirectoriesDev
                        .FirstOrDefault(localDir => localDir.Contains(devFolderName, StringComparison.OrdinalIgnoreCase));

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
