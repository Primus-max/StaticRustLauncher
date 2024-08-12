using System.Diagnostics;

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

                HostingsList.SelectedItem = null;
            }
        }
    }
    private void VisitHost_Click(object sender, RoutedEventArgs e)
    {        
        var button = sender as Button;
        try
        {
            if (button?.DataContext is Hosting hosting)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = hosting.Url,
                    UseShellExecute = true
                });
            }

        }
        catch (Exception)
        {
            // TODO Логирование
        }
        finally
        {
            e.Handled = true;
        }
    }
}
