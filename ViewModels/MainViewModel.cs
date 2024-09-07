namespace StaticRustLauncher.ViewModels;

public class MainViewModel : BaseViewModel
{
    #region Команды
    public ICommand NavigationCommand { get; } = null!;
    public ICommand CloseAppCommand { get; } = null!;
    public ICommand MinimizeAppCommand { get; } = null!;
    public ICommand LoginCommand { get; } = null!;
    public ICommand SteamNickNameCommand { get; } = null!;
    #endregion

    #region Приватные поля
    private Frame Frame { get; } = null!;
    private System.Windows.Controls.UserControl _currentPanel = null!;
    private System.Windows.Controls.UserControl _statisticsPanel = null!;
    private string? _activeButton = null!;
    private StatisticsData _statisticsData = null!;
    private string? _actualVersion = null!;
    private string? _currentVersion = null!;
    private bool _availableNewVersionClient = false;
    private bool _isDownloading = false;
    private string _currentPage = "Home";
    #endregion

    #region Публичные поля
    public System.Windows.Controls.UserControl CurrentPanel
    {
        get => _currentPanel;
        set => Set(ref _currentPanel, value);
    }
    public System.Windows.Controls.UserControl StatisticsPanel
    {
        get => _statisticsPanel;
        set => Set(ref _statisticsPanel, value);
    }
    public string? ActiveButton
    {
        get => _activeButton;
        set => Set(ref _activeButton, value);
    }
    public StatisticsData StatisticsData
    {
        get => _statisticsData;
        set => Set(ref _statisticsData, value);
    }
    public string? ActualVersion
    {
        get => _actualVersion;
        set => Set(ref _actualVersion, value);
    }
    public string? CurrentVersion => GameVersions.CurrentVersion;

    #endregion

    public MainViewModel(Frame frame)
    {
        EventBus.DownloadStarted += OnDownloadStarted;
        EventBus.DownloadCompleted += OnDownloadCompleted;
        EventBus.UpdateAvailable += OnUpdateAvailable;

        NavigationCommand = new LambdaCommand(OnNavigate);
        CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted);
        MinimizeAppCommand = new LambdaCommand(OnMinimizeAppCommandExecuted);
        LoginCommand = new LambdaCommand(OnLoginOpenWindow);
        SteamNickNameCommand = new LambdaCommand(OnSteamNickName);

        Frame = frame;
        Frame.Navigate(new HomePage()); // Инициализация начальной страницы      

        ShowStatisticsPanel();
        Task.Run(async () => await InitPanelAsync());
        UpdateCheckerService.StartAutoCheck();
    }

    private async Task InitPanelAsync()
    {        
        await CheckNewVersionGame();
        SetPanelGame();
    }

    // Навигация
    private void OnNavigate(object viewName)
    {
        if (viewName is string destination)
            ActiveButton = destination;

        _currentPage = viewName as string ?? string.Empty;
        switch (_currentPage)
        {
            case "Home":
                {
                    Frame.Navigate(new HomePage());
                    SetPanelGame();
                    ShowStatisticsPanel();
                    break;
                }
            case "News":
                {
                    Frame.Navigate(new NewsPage());
                    HideGamePanel();
                    HideStatisticsPanel();
                    break;
                }
            case "Hosts":
                {
                    Frame.Navigate(new HostingPage());
                    HideStatisticsPanel();
                    CurrentPanel = new System.Windows.Controls.UserControl();
                    break;
                }
            case "Settings":
                {
                    Frame.Navigate(new SettingsPage());
                    HideGamePanel();
                    HideStatisticsPanel();
                    break;
                }
            default:
                break;
        }
    }

    // Показывает нужную панель для режимы игры
    private void SetPanelGame()
    {
        if (_availableNewVersionClient && !_isDownloading && _currentPage == "Home")
           System.Windows.Application.Current.Dispatcher.Invoke(() => ShowAvailableNewVersionPanel());
        else if (_isDownloading && _currentPage == "Home")
            System.Windows.Application.Current.Dispatcher.Invoke(() => ShowLoadingPanel());
        else
        {
            if(_currentPage == "Home")
                System.Windows.Application.Current.Dispatcher.Invoke(() => ShowPlayNowPanel());
        }
            
    }

    private async Task CheckNewVersionGame()
    {
        var actualVersionTulip = await UpdateCheckerService.IsUpdateAvailableAsync();
        ActualVersion = actualVersionTulip.Item2 ?? string.Empty;
        _availableNewVersionClient = actualVersionTulip.Item1;
    }

    #region Действия на подписки событий
    private void OnDownloadStarted()
    {
        ShowLoadingPanel();
        _isDownloading = true;
    }

    private void OnDownloadCompleted()
    {
       System.Windows.Application.Current.Dispatcher.Invoke(async () =>
        {
            _isDownloading = false;
            await InitPanelAsync();
        });
    }

    private void OnUpdateAvailable()
    {
        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            if (!_isDownloading)
                ShowAvailableNewVersionPanel();
        });
    }
    #endregion

    #region Панели (играть/скачать/статистика)
    private void ShowAvailableNewVersionPanel() => CurrentPanel = new AvailableNewVersionControl();
    private void ShowLoadingPanel()
    {        
        if (IsHomePageActive())
        {            
            CurrentPanel = new LoadingPanelControl();
        }
    }
    private void ShowInstallGamePanel() => CurrentPanel = new InstallGameControl();
    private void ShowPlayNowPanel() => CurrentPanel = new PlayNowControl();
    private void HideGamePanel() => CurrentPanel = null!;

    private void ShowStatisticsPanel() => StatisticsPanel = new StatisticsControl();
    private void HideStatisticsPanel() => StatisticsPanel = null!;

    #endregion

    #region Методы команд
    private void OnCloseAppCommandExecuted(object parameter)
    {
        ConfirmExitWindow confirmExitWindow = new();
        WindowHelper.OpenWindowWithBlur(confirmExitWindow);
    }

    private void OnMinimizeAppCommandExecuted(object parameter) =>
        System.Windows.Application.Current.MainWindow.WindowState = WindowState.Minimized;

    private void OnLoginOpenWindow(object parameter)
    {
        // Окно владельцем для центрирования
        var loginWindow = new LoginWindow
        {
            Owner = System.Windows.Application.Current.MainWindow
        };

        WindowHelper.OpenWindowWithBlur(loginWindow);
    }

    private void OnSteamNickName(object parameter)
    {
       System.Windows.MessageBox.Show("Что про никнейм в стиме");
    }
    #endregion

    private bool IsHomePageActive() =>
        _currentPage is "Home";
}
