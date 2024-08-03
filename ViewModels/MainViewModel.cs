using StaticRustLauncher.Infrastructure.Commands;
using StaticRustLauncher.Resources.Controls;
using StaticRustLauncher.ViewModels.Helpers;
using StaticRustLauncher.Views.Pages;
using StaticRustLauncher.Views.Windows;

using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace StaticRustLauncher.ViewModels;

public class MainViewModel : BaseViewModel
{
    #region Команды
    public ICommand NavigationCommand { get; } = null!;
    public ICommand CloseAppCommand { get; } = null!;
    public ICommand MinimizeAppCommand { get; } = null!;
    public ICommand LoginCommand { get; } = null!;
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
        LoginCommand = new LambdaCommand(OnLoginOpenWindow);

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

    private void OnLoginOpenWindow(object parameter)
    {
        // Окно владельцем для центрирования
        var loginWindow = new LoginWindow
        {
            Owner = Application.Current.MainWindow
        };

        // Эффект размытия к главному окну с анимацией
        var mainWindow = Application.Current.MainWindow;
        BlurEffectHelper.ApplyBlurEffect(mainWindow, 0, 10, 0.5);

        loginWindow.ShowDialog();

        BlurEffectHelper.RemoveBlurEffect(mainWindow, 10, 0, 0.5);
    }

    #endregion
}
