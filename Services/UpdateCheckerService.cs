namespace StaticRustLauncher.Services;

public class UpdateCheckerService
{
    private static readonly HttpClient httpClient = new();
    private const string VersionUrl = "http://194.147.90.218/client/version";    

    /// <summary>
    /// Проверяет, доступна ли новая версия игры.
    /// </summary>
    /// <returns>True, если доступна новая версия; иначе, false.</returns>
    public static async Task<(bool, string)> IsUpdateAvailableAsync()
    {
        string? latestVersion = await GetLatestVersionAsync();
        if (latestVersion == null)        
            return (false , latestVersion);        

        string? currentVersion = GetCurrentVersion();
        if (currentVersion == null || 
            !latestVersion.Equals(currentVersion, StringComparison.OrdinalIgnoreCase))       
            return (true, latestVersion);         

        return (true, latestVersion); 
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
        SettingsApp.Load();
        string localVersionFile = SettingsApp.DirGame;

        try
        {
            if (File.Exists(localVersionFile))
            {
                string json = File.ReadAllText(localVersionFile);
                var versions = JsonSerializer.Deserialize<List<string>>(json);
                return versions?.Count > 0 ? versions[0] : null; 
            }
            return null;
        }
        catch (Exception)
        {          
            return null;
        }
    }
}
