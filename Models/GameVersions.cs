namespace StaticRustLauncher.Models;

public class GameVersions
{
    private static readonly string _filePath = "game_v.json";
    public static ObservableCollection<string> VersionList { get; private set; } = [];

    static GameVersions() => LoadVersions();


    private static void LoadVersions()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                var versions = JsonSerializer.Deserialize<List<string>>(json);
                if (versions != null)                
                    VersionList = new ObservableCollection<string>(versions);
                
            }
            catch (Exception)
            {

            }
        }
    }

    public static void AddVersion(string version)
    {
        if (!VersionList.Contains(version))
        {
            VersionList.Add(version);
            SaveVersions();
        }
    }

    private static void SaveVersions()
    {
        try
        {
            string json = JsonSerializer.Serialize(VersionList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch (Exception)
        {

        }
    }

    public static string? CurrentVersionClient => VersionList.Count > 0 ? VersionList[0] : string.Empty;
}