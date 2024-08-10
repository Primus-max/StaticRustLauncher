namespace StaticRustLauncher.Views.Pages;

/// <summary>
/// Логика взаимодействия для HostingPage.xaml
/// </summary>
public partial class HostingPage : Page
{

    public HostingPage()
    {
        InitializeComponent();
    }


    private void OpenDescriptionHost_Click(object sender, SelectionChangedEventArgs e)
    {

        if (DataContext is HostsViewModel viewModel)
        {
            var selectedHosting = viewModel.SelectedHosting;
            if (selectedHosting != null)
            {
                var newsDetailsWindow = new HostsWindow(selectedHosting);
                WindowHelper.OpenWindowWithBlur(newsDetailsWindow);
            }
        }
    }
}
