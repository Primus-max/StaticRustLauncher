using System.Windows.Navigation;

namespace StaticRustLauncher.Views.Windows;

/// <summary>
/// Логика взаимодействия для ServerDetailWindow.xaml
/// </summary>
public partial class ServerDetailWindow : Window
{
    private readonly Server _selectedServer = null!;

    public ServerDetailWindow(Server selectedServer)
    {
        InitializeComponent();
        DataContext = new ServerDetailViewModel(selectedServer);
        _selectedServer = selectedServer;
    }


    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        // Открывает ссылку в браузере по умолчанию
        Process.Start(new ProcessStartInfo
        {
            FileName = e.Uri.AbsoluteUri,
            UseShellExecute = true
        });
        e.Handled = true;
    }

    // Копировать текст в буфер обмена
    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Clipboard.SetText($"client.connect {_selectedServer.Ip}:{_selectedServer.Port}");
    }

    private void CloseButton_Click(object sender, MouseButtonEventArgs e)
    {
        this.Close();
    }
}
