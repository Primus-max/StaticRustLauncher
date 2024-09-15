namespace StaticRustLauncher.EventHandlers;

public static class EventBus
{
    public static event Action<double> DownloadProgressChanged;
    public static event Action? UpdateAvailable;
    public static event Action? DownloadStarted;
    public static event Action? DownloadCompleted;
    public static event Action? CancelDownloading;

    public static void OnUpdateAvailable() =>
        UpdateAvailable?.Invoke();

    public static void NotifyDownloadProgressChanged(double progress)=>    
        DownloadProgressChanged?.Invoke(progress);    


    public static void OnDownloadStarted() =>    
        DownloadStarted?.Invoke();

    public static void OnDownloadCompleted() 
        => DownloadCompleted?.Invoke();

    public static void OnCancelDownloading() =>
        CancelDownloading?.Invoke();
}

