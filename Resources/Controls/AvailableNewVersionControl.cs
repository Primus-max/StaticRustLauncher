namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для AvalibleNewVirsionControl.xaml
/// </summary>
public partial class AvailableNewVersionControl : UserControl
{
    public AvailableNewVersionControl()
    {
        InitializeComponent();
    }

    public void DownloadNewVirsion_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Скачивание новой версии началось");
    }
}
