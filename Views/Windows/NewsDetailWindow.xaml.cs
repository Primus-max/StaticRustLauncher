namespace StaticRustLauncher.Views.Windows;

/// <summary>
/// Логика взаимодействия для NewsDetailWindow.xaml
/// </summary>
public partial class NewsDetailWindow : Window
{
    public NewsDetailWindow(News selectedNews)
    {
        InitializeComponent();
        DataContext = new NewsDetailWindowViewModel(selectedNews);
    }

    public void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();        
    }
}
