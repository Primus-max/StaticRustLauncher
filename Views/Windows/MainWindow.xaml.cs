namespace StaticRustLauncher.Views.Windows;

/// <summary>
/// Главное окно приложения
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(MainFrame);

        AuthorizationButton.Visibility = Visibility.Visible;
    }


    public void ShowPostLoginButtons(bool isLogin)
    {
        AuthorizationButton.Visibility = Visibility.Collapsed;
        UserCabinetButton.Visibility = Visibility.Visible;
        LogoutButton.Visibility = Visibility.Visible;
    }
}