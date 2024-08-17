namespace StaticRustLauncher.Models;

public class GameVersions
{
    private static List<string>? _versionList;
    private static string? _filePath = "game_v.json";

    public static List<string> VersionList
    {
        get
        {
            if (_versionList == null)
            {
                LoadVersions();
            }
            return _versionList ?? new List<string>();
        }
    }

    public static string? CurrentVersion
    {
        get => _currentVersion;
        set
        {
            if (value != null && !VersionList.Contains(value))
            {
                _versionList?.Add(value);
                _currentVersion = value;
                SaveVersions();
            }
        }
    }

    private static string? _currentVersion;

    private static void LoadVersions()
    {       
        if (File.Exists(_filePath))
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                _versionList = JsonSerializer.Deserialize<List<string>>(json);
            }
            catch (Exception )
            {
                
            }
        }
    }

    private static void SaveVersions()
    {        
        try
        {
            string json = JsonSerializer.Serialize(_versionList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch (Exception )
        {
            
        }
    }
}
