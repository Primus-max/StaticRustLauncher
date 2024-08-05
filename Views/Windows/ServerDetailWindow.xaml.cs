using System.Diagnostics;
using System.Windows.Navigation;

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

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Копировать текст в буфер обмена
        Clipboard.SetText("255.255.255");
    }

    private void CloseButton_Click(object sender, MouseButtonEventArgs e)
    {
        this.Close();
    }
}
