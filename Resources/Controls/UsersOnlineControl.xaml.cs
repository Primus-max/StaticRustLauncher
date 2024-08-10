namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для UsersOnlineControl.xaml
/// </summary>
public partial class UsersOnlineControl : UserControl
{
    public static readonly DependencyProperty UsersOnlineProperty =
       DependencyProperty.Register("UsersOnline", typeof(int), typeof(UsersOnlineControl), new PropertyMetadata(0));

    public int UsersOnline
    {
        get => (int)GetValue(UsersOnlineProperty);
        set => SetValue(UsersOnlineProperty, value);
    }

    public UsersOnlineControl()
    {
        InitializeComponent();        
    }
}
