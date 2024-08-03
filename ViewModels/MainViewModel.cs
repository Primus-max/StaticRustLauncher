using StaticRustLauncher.Infrastructure.Commands;
using StaticRustLauncher.Resources.Controls;
using StaticRustLauncher.Views.Pages;

namespace StaticRustLauncher.ViewModels;

public class MainViewModel : BaseViewModel
{
    #region Команды
    public ICommand NavigationCommand { get; }
    public ICommand CloseAppCommand { get; }
    public ICommand MinimizeAppCommand { get; }
    #endregion


    private Frame Frame { get; } = null!;
    private UserControl _currentPanel = null!;
    public UserControl CurrentPanel 
    { 
        get => _currentPanel; 
        set => Set(ref _currentPanel,  value); 
    }

    public MainViewModel(Frame frame)
    {
        NavigationCommand = new LambdaCommand(OnNavigate);
        CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted);
        MinimizeAppCommand = new LambdaCommand(OnMinimizeAppCommandExecuted);

        Frame = frame;
        Frame.Navigate(new HomePage()); // Инициализация начальной страницы
        ShowPlayNowPanel();
    }


    private void OnNavigate(object viewName)
    {
        switch (viewName as string)
        {
            case "Home":
                Frame.Navigate(new HomePage());
                break;
            case "Settings":
                Frame.Navigate(new SettingsPage());
                break;
            default:
                break;
        }
    }

    private void ShowAvailableNewVersionPanel() =>    
        CurrentPanel = new AvailableNewVersionControl();    

    private void ShowLoadingPanel() =>    
        CurrentPanel = new LoadingPanelControl();

    private void ShowInstallGamePanel() =>
        CurrentPanel = new InstallGameControl();

    private void ShowPlayNowPanel() =>
        CurrentPanel = new PlayNowControl();


    #region Методы команд
    private void OnCloseAppCommandExecuted(object parameter) =>
    App.Current.Shutdown();

    private void OnMinimizeAppCommandExecuted(object parameter) =>
        Application.Current.MainWindow.WindowState = WindowState.Minimized;
    #endregion
}
