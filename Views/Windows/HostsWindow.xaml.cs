namespace StaticRustLauncher.Views.Windows;

/// <summary>
/// Логика взаимодействия для HostingsWindow.xaml
/// </summary>
public partial class HostsWindow : Window
{
    public Host? SelectedHost { get; set; } = null!;

    public HostsWindow(Host selectedHost)
    {
        InitializeComponent();
        DataContext = new HostsWindowViewModel(selectedHost);
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) =>    
        this.Close();
    
}
