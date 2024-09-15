namespace StaticRustLauncher.Services;

public class UpdateCheckerService
{
    private static readonly HttpClient httpClient = new();
    private const string VersionUrl = "http://194.147.90.218/client/version";    // http://194.147.90.218/launcher/version
    private static System.Threading.Timer? _timer;
    private static readonly TimeSpan CheckInterval = TimeSpan.FromMinutes(10);
       

    /// <summary>
    /// Проверяет, доступна ли новая версия игры.
    /// </summary>
    /// <returns>True, если доступна новая версия; иначе, false.</returns>
    public static async Task<(bool, string)> IsUpdateAvailableAsync()
    {
        string? latestVersion = await GetLatestVersionAsync();
        if (latestVersion == null)
            return (false, latestVersion);

        string? currentVersion = GetCurrentVersion();
        return currentVersion == null ||
            !latestVersion.Equals(currentVersion, StringComparison.OrdinalIgnoreCase)
            ? ((bool, string))(true, latestVersion)
            : ((bool, string))(true, latestVersion);
    }

   /// <summary>
   /// Запускается каждое указанное время для проверки и установки обновлений
   /// </summary>
    public static void StartAutoCheck()
    {
        _timer = new System.Threading.Timer(async _ => await CheckForUpdateAsync(), null, 0, (int)CheckInterval.TotalMilliseconds);
    }

   
    private static void StopAutoCheck()
    {
        _timer?.Change(Timeout.Infinite, 0);
        _timer?.Dispose();
    }

    public static async Task<string?> GetLatestVersionAsync()
    {
        try
        {
            string response = await httpClient.GetStringAsync(VersionUrl);
            return response.Trim();
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }

    private static string? GetCurrentVersion()
    {
        //SettingsApp.Load();
        string localVersionFile = SettingsApp.DirGame + "\\version.txt";

        try
        {
            if (File.Exists(localVersionFile))
            {
                var lines = File.ReadAllLines(localVersionFile);
                string? version = lines[0];
                return version ?? string.Empty;
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static async Task CheckForUpdateAsync()
    {
        var (isUpdateAvailable, latestVersion) = await IsUpdateAvailableAsync();
        if (isUpdateAvailable)
            EventBus.OnUpdateAvailable();        
    }
}
