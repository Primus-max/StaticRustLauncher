namespace StaticRustLauncher.Models;

public class Host
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Users { get; set; }
    public int Projects { get; set; }
    public HostingType HostingType { get; set; }
}

public enum HostingType
{
    OurChoice,
    Reliable
}
