using System.Net;

namespace StaticRustLauncher.Models;

public class Server
{
    public IPAddress? ServerAddress { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int PlayerOnline { get; set; }
    public ServerType? ServerType { get; set; }
    public string? ServerCategory { get; set; }
    public string? Country { get; set; }
    public string? Status { get; set; }
    public int Version { get; set; }
    public string? MapName { get; set; }
}


public enum ServerType
{
    Premium,
    Vip
}