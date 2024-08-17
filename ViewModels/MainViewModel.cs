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
    private UserControl _currentPanel = null!;
    private UserControl _statisticsPanel = null!;
    private string? _activeButton = null!;
    private StatisticsData _statisticsData = null!;
    private string? _actualVersionClient = null!;
    private string? _currentVersionClient = null!;
    private bool _availableNewVersionClient = false;
    #endregion

    #region Публичные поля
    public UserControl CurrentPanel
    {
        get => _currentPanel;
        set => Set(ref _currentPanel, value);
    }
    public UserControl StatisticsPanel
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
    public string? ActualVersionClient
    {
        get => _actualVersionClient;
        set => Set(ref _actualVersionClient, value);
    }
    public string? CurrentVersionClient => GameVersions.CurrentVersionClient;

    #endregion

    public MainViewModel(Frame frame)
    {
        NavigationCommand = new LambdaCommand(OnNavigate);
        CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted);
        MinimizeAppCommand = new LambdaCommand(OnMinimizeAppCommandExecuted);
        LoginCommand = new LambdaCommand(OnLoginOpenWindow);
        SteamNickNameCommand = new LambdaCommand(OnSteamNickName);

        Frame = frame;
        Frame.Navigate(new HomePage()); // Инициализация начальной страницы

        ShowStatisticsPanel();
        Task.Run(async () => await InitPanelAsync());
    }

    private async Task InitPanelAsync()
    {
        var actualVersionTulip = await UpdateCheckerService.IsUpdateAvailableAsync();
        ActualVersionClient = actualVersionTulip.Item2 ?? string.Empty;
        _availableNewVersionClient = actualVersionTulip.Item1;

        SetPanelGame();
    }

    // Навигация
    private void OnNavigate(object viewName)
    {
        if (viewName is string destination)
            ActiveButton = destination;

        switch (viewName as string)
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
                    CurrentPanel = new UserControl();
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
        if (_availableNewVersionClient)
            Application.Current.Dispatcher.Invoke(() => ShowAvailableNewVersionPanel());
        else
            Application.Current.Dispatcher.Invoke(() => ShowPlayNowPanel());
    }

    #region Панели (играть/скачать/статистика)
    private void ShowAvailableNewVersionPanel() => CurrentPanel = new AvailableNewVersionControl();
    private void ShowLoadingPanel() => CurrentPanel = new LoadingPanelControl();
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
        Application.Current.MainWindow.WindowState = WindowState.Minimized;

    private void OnLoginOpenWindow(object parameter)
    {
        // Окно владельцем для центрирования
        var loginWindow = new LoginWindow
        {
            Owner = Application.Current.MainWindow
        };

        WindowHelper.OpenWindowWithBlur(loginWindow);
    }

    private void OnSteamNickName(object parameter) 
    {
        MessageBox.Show("Что про никнейм в стиме");
    }
    #endregion
}
