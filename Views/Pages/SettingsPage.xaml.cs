namespace StaticRustLauncher.Views.Pages;

/// <summary>
/// Логика взаимодействия для SettingsPage.xaml
/// </summary>
public partial class SettingsPage : Page
{
    private object _lastSelectedItem;
    public SettingsPage()
    {
        InitializeComponent();
        DataContext = new SettingsViewModel();        
    }

    private void SelectedVersionGame(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as System.Windows.Controls.ComboBox;
        if (comboBox == null)
            return;

        var selectedItem = comboBox.SelectedItem;
        
        if (!Equals(selectedItem, SettingsApp.GameVersion))
        {
            _lastSelectedItem = selectedItem;
            
            var viewModel = (SettingsViewModel)DataContext;
            if (viewModel != null && selectedItem != null)
            {
                viewModel.CheckSelectedVersionGame();
            }
        }
    }
}
