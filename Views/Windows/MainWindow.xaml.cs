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
    }


    public void ShowPostLoginButtons(bool isLoggined)
    {
        var asdf = "Kjslkdjf";
    }
}