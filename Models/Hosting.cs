namespace StaticRustLauncher.Models;

public class Hosting
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int UsersCount { get; set; }
    public int ProjectsCount { get; set; }
    public HostingType HostingType { get; set; }

}

public enum HostingType
{
    OurChoice,
    Reliable
}
