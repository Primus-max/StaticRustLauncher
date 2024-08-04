namespace StaticRustLauncher.Models;

public class News
{
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime? ReleaseDate { get; set; }
    public ServerType? ServerTypeForBackGround { get; set; } = ServerType.Premium; 

}
