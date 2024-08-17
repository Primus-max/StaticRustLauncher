namespace StaticRustLauncher.Models;

public static class SettingsApp
{
    public static string Lang { get; set; } = "ru";
    public static string GameVersion { get; set; } = "actual";
    public static string DirLauncher { get; set; } = string.Empty;
    public static string DirGame { get; set; } = string.Empty;

    private static readonly string settingsFilePath = "settings.json";

    public static void Load()
    {
        if (File.Exists(settingsFilePath))
        {
            try
            {
                string json = File.ReadAllText(settingsFilePath);
                var loadedSettings = JsonSerializer.Deserialize<Settings>(json);

                if (loadedSettings != null)
                {
                    Lang = loadedSettings?.Lang;
                    GameVersion = loadedSettings?.GameVersion;
                    DirLauncher = loadedSettings?.DirLauncher;
                    DirGame = loadedSettings?.DirGame;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}");
            }
        }
    }

    public static void Save()
    {
        try
        {
            var settings = new Settings
            {
                Lang = Lang,
                GameVersion = GameVersion,
                DirLauncher = DirLauncher,
                DirGame = DirGame
            };

            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving settings: {ex.Message}");
        }
    }

    private class Settings
    {
        public string? Lang { get; set; }
        public string? GameVersion { get; set; }
        public string? DirLauncher { get; set; }
        public string? DirGame { get; set; }
    }
}

