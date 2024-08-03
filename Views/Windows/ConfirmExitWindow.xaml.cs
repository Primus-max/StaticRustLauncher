namespace StaticRustLauncher.Views.Windows;

/// <summary>
/// Логика взаимодействия для ConfirmExitWindow.xaml
/// </summary>
public partial class ConfirmExitWindow : Window
{
    public ConfirmExitWindow()
    {
        InitializeComponent();
        DataContext = new ConfirmExitViewModel();
    }
}
