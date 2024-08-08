namespace StaticRustLauncher.Models;

public class HostingCollection
{
    public List<Hosting>? Hostings { get; set; }
}

public class Hosting
{
    public string? Name { get; set; }
    public string? Desctiption { get; set; }  // Обратите внимание, что в JSON ключ "Desctiption" с ошибкой, это намеренно?
    public string? Image { get; set; }
    public int Users { get; set; }
    public int Projects { get; set; }
    public string? Url { get; set; }
    public int Status { get; set; }
    public HostingType HostingType { get; set; }
}

public enum HostingType
{
    OurChoice,
    Reliable
}
