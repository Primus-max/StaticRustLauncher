namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для PlayNowControl.xaml
/// </summary>
public partial class PlayNowControl : UserControl
{
    public PlayNowControl()
    {
        InitializeComponent();
        DataContext = new PlayNowViewModel();
    }
}
