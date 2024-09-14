using System.Text;
using MessageBox = System.Windows.Forms.MessageBox;

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

    private void PlayStandardGame_OnClick(object sender, RoutedEventArgs e)
    {
        if(SettingsApp.OldVersions is null) return;
        SettingsApp.OldVersions.TryGetValue(SettingsApp.GameVersion , out string selectedGameVersionPath);
        
        if(string.IsNullOrEmpty(selectedGameVersionPath)) return;

        var executableFilePath = $"{selectedGameVersionPath}\\RustClient.exe";
        
        StartGame(executableFilePath);
    }
    
    private void StartGame(string path)
    {
        if (!File.Exists(path))
        {
            MessageBox.Show(
                $"Не удалось запустить файл по пути {path}, проверьте правильность пути и исполняемого файла или обратитесь к разработчикам");
            return;
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });

        }
        catch (Exception e)
        {
            Log.Fatal($"Ошибка при старте игры {e.Message}");
            MessageBox.Show(
                $"Не удалось запустить игру из за этой ошибки {e} , если не сможете устранить её самостоятельно, обратитесь к разработчикам");
        }
    }
}
