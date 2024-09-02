namespace StaticRustLauncher.EventHandlers;

public static class EventBus
{
    public static event Action<int> DownloadProgressChanged;
    public static event Action? UpdateAvailable;
    public static event Action? DownloadStarted;
    public static event Action? DownloadCompleted;

    public static void OnUpdateAvailable() =>
        UpdateAvailable?.Invoke();

    public static void NotifyDownloadProgressChanged(int progress)=>    
        DownloadProgressChanged?.Invoke(progress);    


    public static void OnDownloadStarted() =>    
        DownloadStarted?.Invoke();

    public static void OnDownloadCompleted() 
        => DownloadCompleted?.Invoke();

}

