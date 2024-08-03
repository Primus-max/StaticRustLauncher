using StaticRustLauncher.Infrastructure.Commands;
using StaticRustLauncher.Views.Pages;

namespace StaticRustLauncher.ViewModels;

public  class MainViewModel : BaseViewModel
{
    /// <summary>
    /// Навигация по страница внутри главного окна
    /// </summary>
    public ICommand NavigationCommand { get; }
    public ICommand CloseAppCommand { get; }
    public ICommand MinimizeAppCommand { get; }
    public MainViewModel(Frame frame)
    {
        NavigationCommand = new LambdaCommand(OnNavigate);
        CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted);
        MinimizeAppCommand = new LambdaCommand(OnMinimizeAppCommandExecuted);

        Frame = frame;
        Frame.Navigate(new HomePage()); // Инициализация начальной страницы
    }

    private Frame Frame { get; }


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

    private void OnCloseAppCommandExecuted(object parameter) =>    
        App.Current.Shutdown();
 
    private void OnMinimizeAppCommandExecuted(object parameter) =>
        Application.Current.MainWindow.WindowState = WindowState.Minimized;
}
