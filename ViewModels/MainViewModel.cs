using StaticRustLauncher.Infrastructure.Commands;
using StaticRustLauncher.Views.Pages;

namespace StaticRustLauncher.ViewModels;

public  class MainViewModel : BaseViewModel
{
    /// <summary>
    /// Навигация по страница внутри главного окна
    /// </summary>
    public ICommand NavigationCommand { get; }

    public MainViewModel(Frame frame)
    {
        NavigationCommand = new LambdaCommand(OnNavigate);
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
}
