namespace StaticRustLauncher.Services;

public class UpdateCheckerService
{
    private static readonly HttpClient httpClient = new();
    private const string VersionUrl = "http://194.147.90.218/client/version";
    private const string LocalVersionFile = "game_v.json";

    /// <summary>
    /// Проверяет, доступна ли новая версия игры.
    /// </summary>
    /// <returns>True, если доступна новая версия; иначе, false.</returns>
    public static async Task<bool> IsUpdateAvailableAsync()
    {
        string? latestVersion = await GetLatestVersionAsync();
        if (latestVersion == null)        
            return false;        

        string? currentVersion = GetCurrentVersion();
        if (currentVersion == null || 
            !latestVersion.Equals(currentVersion, StringComparison.OrdinalIgnoreCase))       
            return true;         

        return false; 
    }

    
    private static async Task<string?> GetLatestVersionAsync()
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
        try
        {
            if (File.Exists(LocalVersionFile))
            {
                string json = File.ReadAllText(LocalVersionFile);
                var versions = JsonSerializer.Deserialize<List<string>>(json);
                return versions?.Count > 0 ? versions[0] : null; // Предполагаем, что первая версия - текущая
            }
            return null;
        }
        catch (Exception)
        {          
            return null;
        }
    }
}
