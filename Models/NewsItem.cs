namespace StaticRustLauncher.Models;

public class NewsItem
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? DateCreate { get; set; }
    public string? ServerTypeForBackGround => "Premium"; // Значение по умолчанию, чтобы получать нужный фон
}
