using System.Text.Json.Serialization;

namespace StaticRustLauncher.Models;

public static class SettingsApp
{
    public static string Lang { get; set; } = "ru";
    public static string GameVersion { get; set; } = "actual";
    public static string DirLauncher { get; set; } = string.Empty;
    public static string DirGame { get; set; } = string.Empty;
    public static string Host { get; set; } = string.Empty;
    public static int Port { get; set; }
    public static string User { get; set; } = string.Empty;
    public static string Pass { get; set; } = string.Empty;
    public static string GamesPath { get; set; } = string.Empty;
    public static Dictionary<string, string>? OldVersions { get; set; } = [];

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
                    Host = loadedSettings.Host;
                    Port = loadedSettings.Port;
                    User = loadedSettings.User;
                    Pass = loadedSettings.Pass;
                    GamesPath = loadedSettings.GamesPath;
                    OldVersions = loadedSettings.OldVersions ?? new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}");
            }
        }
    }

    public static void AddGameVersion(string version, string path)
    {
        if (!OldVersions.ContainsKey(version))
        {
            OldVersions.Add(version, path);
            Save();
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
                DirGame = DirGame,
                Host = Host,
                GamesPath = GamesPath,
                OldVersions = OldVersions,
                Pass = Pass,
                Port = Port,
                User = User,
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
        [JsonPropertyName("lang")]
        public string? Lang { get; set; }

        [JsonPropertyName("gameversion")]
        public string? GameVersion { get; set; }

        [JsonPropertyName("dir_launcher")]
        public string? DirLauncher { get; set; }

        [JsonPropertyName("dir_game")]
        public string? DirGame { get; set; }

        [JsonPropertyName("host")]
        public string? Host { get; set; } = string.Empty;

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("user")]
        public string? User { get; set; } = string.Empty;

        [JsonPropertyName("pass")]
        public string? Pass { get; set; } = string.Empty;

        [JsonPropertyName("gamesPath")]
        public string? GamesPath { get; set; } = string.Empty;

        [JsonPropertyName("oldVersions")]
        public Dictionary<string, string>? OldVersions { get; set; }
    }
}

