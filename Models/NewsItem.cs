namespace StaticRustLauncher.Models;

public class NewsItem
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? DateCreate { get; set; }
    public ServerType? ServerTypeForBackGround { get; set; } = ServerType.Premium; // Значение по умолчанию, чтобы получать нужный фон
}
