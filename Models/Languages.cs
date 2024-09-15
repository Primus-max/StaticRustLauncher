namespace StaticRustLauncher.Models;

public class Languages
{
    private static readonly string _filePath = "languages.json";
    public static ObservableCollection<string> LanguageList { get; private set; } = [];

    static Languages() =>    
        LoadLanguages();
    
    private static void LoadLanguages()
    {        
        if (File.Exists(_filePath))
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                var languages = JsonSerializer.Deserialize<List<string>>(json);
                if (languages != null)                
                    LanguageList = new ObservableCollection<string>(languages);
                
            }
            catch (Exception)
            {
                
            }
        }
    }
}