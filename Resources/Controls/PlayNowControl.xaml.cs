namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для PlayNowControl.xaml
/// </summary>
public partial class PlayNowControl : System.Windows.Controls.UserControl
{
    public PlayNowControl()
    {
        InitializeComponent();
        DataContext = new PlayNowViewModel();
    }
}
