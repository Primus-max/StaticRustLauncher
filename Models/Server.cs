namespace StaticRustLauncher.Models;

public class ServerCollection
{
    public List<Server>? Servers { get; set; }
}

public class Server
{
    public string? Hostname { get; set; }
    public string? Map { get; set; }
    public int Players { get; set; }
    public int Maxplayers { get; set; }
    public string? Tags { get; set; }
    public string? Ip { get; set; }
    public int Port { get; set; }
    public int QueryPort { get; set; }
    public string? Description { get; set; }
    public string? Headerimage { get; set; }
    public string? Status { get; set; }
    public List<URL>? URL { get; set; }
}

public class URL
{
    public string? VK { get; set; }
    public string? Discord { get; set; }
    public string? Telegram { get; set; }
    public string? Steam { get; set; }
    public string? Site { get; set; }
}

public enum ServerType
{
    Premium,
    Vip
}