namespace StaticRustLauncher.ViewModels;

public class MainViewModel : BaseViewModel
{
    #region Команды
    public ICommand NavigationCommand { get; } = null!;
    public ICommand CloseAppCommand { get; } = null!;
    public ICommand MinimizeAppCommand { get; } = null!;
    public ICommand LoginCommand { get; } = null!;
    #endregion

    #region Приватные поля
    private Frame Frame { get; } = null!;
    private UserControl _currentPanel = null!;
    private UserControl _statisticsPanel = null!;
    private string? _activeButton = null!;
    private StatisticsData _statisticsData = null!;   
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
    #endregion

    public MainViewModel(Frame frame)
    {
        NavigationCommand = new LambdaCommand(OnNavigate);
        CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted);
        MinimizeAppCommand = new LambdaCommand(OnMinimizeAppCommandExecuted);
        LoginCommand = new LambdaCommand(OnLoginOpenWindow);

        Frame = frame;
        Frame.Navigate(new HomePage()); // Инициализация начальной страницы
        ShowAvailableNewVersionPanel();
        ShowStatisticsPanel();       

        //StatisticsData = new StatisticsData
        //{
        //    UsersOnline = 150,
        //    ServersCount = 25
        //};
    }


    private void OnNavigate(object viewName)
    {
        if (viewName is string destination)
            ActiveButton = destination;

        switch (viewName as string)
        {
            case "Home":
                {
                    Frame.Navigate(new HomePage());
                    ShowPlayNowPanel();
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

    private void ShowAvailableNewVersionPanel() => CurrentPanel = new AvailableNewVersionControl();
    private void ShowLoadingPanel() => CurrentPanel = new LoadingPanelControl();
    private void ShowInstallGamePanel() => CurrentPanel = new InstallGameControl();
    private void ShowPlayNowPanel() => CurrentPanel = new PlayNowControl();
    private void HideGamePanel() => CurrentPanel = null!;

    private void ShowStatisticsPanel() => StatisticsPanel = new StatisticsControl();
    private void HideStatisticsPanel() => StatisticsPanel = null!;
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
    #endregion
}
