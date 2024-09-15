namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для StatisticsControl.xaml
/// </summary>
public partial class StatisticsControl : System.Windows.Controls.UserControl
{

    public static readonly DependencyProperty UsersOnlineProperty =
        DependencyProperty.Register("UsersOnline", typeof(int), typeof(StatisticsControl), new PropertyMetadata(0));

    public int UsersOnline
    {
        get => (int)GetValue(UsersOnlineProperty);
        set => SetValue(UsersOnlineProperty, value);
    }

    public static readonly DependencyProperty ServersCountProperty =
    DependencyProperty.Register("ServersCount", typeof(int), typeof(StatisticsControl), new PropertyMetadata(0));

    public int ServersCount
    {
        get => (int)GetValue(ServersCountProperty);
        set => SetValue(ServersCountProperty, value);
    }

    public StatisticsControl()
    {
        InitializeComponent();
    }
}
