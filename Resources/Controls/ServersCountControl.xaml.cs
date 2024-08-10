namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для ServersCountControl.xaml
/// </summary>
public partial class ServersCountControl : UserControl
{
    public static readonly DependencyProperty ServersCountProperty =
        DependencyProperty.Register("ServersCount", typeof(int), typeof(ServersCountControl), new PropertyMetadata(0));

    public int ServersCount
    {
        get => (int)GetValue(ServersCountProperty);
        set => SetValue(ServersCountProperty, value);
    }


    public ServersCountControl()
    {
        InitializeComponent();        
    }
}
