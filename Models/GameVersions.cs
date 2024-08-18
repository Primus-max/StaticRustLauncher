namespace StaticRustLauncher.Models;

public class GameVersions
{
    public static string? CurrentVersion
    {
        get
        {
            SettingsApp.Load();
            string filePath = $"{SettingsApp.DirGame}\\version.txt" ;
            if (File.Exists(filePath))
            {
                try
                {
                    return File.ReadAllText(filePath).Trim();
                }
                catch (Exception)
                {                   
                    return string.Empty;
                }
            }

            return string.Empty;
        }
    }
}