using StaticRustLauncher.EventHandlers;

namespace StaticRustLauncher.Resources.Controls;

/// <summary>
/// Логика взаимодействия для LoadingPanelControl.xaml
/// </summary>
public partial class LoadingPanelControl : UserControl, INotifyPropertyChanged
{
    private string _percents = "0";    
    public string? CurrentVersion => GameVersions.CurrentVersion;
    public string Percents
    {
        get => _percents;
        set
        {
            if (_percents != value)
            {
                _percents = value;
                OnPropertyChanged(); // Уведомление об изменении свойства
            }
        }
    }

    public LoadingPanelControl()
    {
        InitializeComponent();
        EventBus.DownloadProgressChanged += OnDownloadProgressChanged;
        DataContext = this;
    }

    private void OnDownloadProgressChanged(double progress)
    {
        Dispatcher.Invoke(() =>
        {
            progress = progress > 100 ? 100 : progress;

            ProgressDownloadFiles.Value = progress;
            ProgressDownloadFiles.IsIndeterminate = progress == 0 || progress == 100;  
            Percents = progress.ToString();
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnCancelUpdatingGame_Click(object sender, RoutedEventArgs e)
        => EventBus.OnCancelDownloading();
    
}
