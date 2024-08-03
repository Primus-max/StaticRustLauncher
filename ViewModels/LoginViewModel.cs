namespace StaticRustLauncher.ViewModels;

class LoginViewModel
{
    public ICommand EnterWithoutLoginCommand { get; } = null!;
    public ICommand LoginCommand {  get; } = null!;


    public LoginViewModel()
    {
        EnterWithoutLoginCommand = new LambdaCommand(OnEnterWithoutLogin);
        LoginCommand = new LambdaCommand(OnLogin);
    }

    private void OnEnterWithoutLogin(object parameter)
    {
        // Закрытие окна без логина
        var window = parameter as Window;
        window?.Close();

        // Дополнительная логика для главного окна
        if (Application.Current.MainWindow is MainWindow mainWindow)
        {
            mainWindow.ShowPostLoginButtons();
        }
    }

    private void OnLogin(object parameter)
    {
        // Закрытие окна после логина
        var window = parameter as Window;
        window?.Close();

        // Дополнительная логика для главного окна
        if (Application.Current.MainWindow is MainWindow mainWindow)
        {
            mainWindow.ShowPostLoginButtons();
        }
    }

}
