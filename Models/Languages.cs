namespace StaticRustLauncher.Models;

public class Languages
{

    private static List<string>? _languageList;

    public static List<string> LanguageList
    {
        get
        {
            if (_languageList == null)
            {
                LoadLanguages();
            }
            return _languageList ?? [];
        }
    }

    private static void LoadLanguages()
    {
        string filePath = "languages.json";
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                _languageList = JsonSerializer.Deserialize<List<string>>(json);
            }
            catch (Exception)
            {
               
            }
        }
    }
}