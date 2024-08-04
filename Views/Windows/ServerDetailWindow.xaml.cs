namespace StaticRustLauncher.Views.Windows;

/// <summary>
/// Логика взаимодействия для ServerDetailWindow.xaml
/// </summary>
public partial class ServerDetailWindow : Window
{
    public ServerDetailWindow(Server selectedServer)
    {
        InitializeComponent();
        DataContext = new ServerDetailViewModel(selectedServer);
    }
}
