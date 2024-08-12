namespace StaticRustLauncher.Views.Windows;

/// <summary>
/// Логика взаимодействия для PlayNowWindow
/// </summary>
public partial class PlayNowWindow : Window
{
    public PlayNowWindow()
    {
        InitializeComponent();
        DataContext = new GameModeViewModel();
    }

    private void CloseButton_Click (object sender, EventArgs e) => this.Close();
}
