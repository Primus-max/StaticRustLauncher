namespace StaticRustLauncher.ViewModels;

class LoginViewModel
{
    public ICommand EnterWithoutLoginCommand { get; } = null!;
    public ICommand LoginCommand { get; } = null!;


    public LoginViewModel()
    {
        EnterWithoutLoginCommand = new LambdaCommand(param => OnLoginAction(param, false));
        LoginCommand = new LambdaCommand( param => OnLoginAction(param, true));
    }       

    private void OnLoginAction(object parameter, bool isLogin)
    {        
        var window = parameter as Window;
        window?.Close();

        // Дополнительная логика для главного окна
        if (System.Windows.Application.Current.MainWindow is MainWindow mainWindow)        
            mainWindow.ShowPostLoginButtons(isLogin);       
    }
}
