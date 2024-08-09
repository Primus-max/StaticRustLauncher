namespace StaticRustLauncher.DesignData
{
    /// <summary>
    /// Класс с тестовыми наборами данных (Mock), если не надо, можно удалить
    /// </summary>
    public class DesignData
    {
        public ObservableCollection<Server> Servers { get; set; } = [];
        public ObservableCollection<NewsItem> NewsCollection { get; set; } = [];
        public ObservableCollection<Hosting> Hosts { get; set; } = [];

        public DesignData()
        {
            GenerateTestServers();
            //LoadNews();
            //LoadHosts();
        }

        public static List<Server> GenerateTestServers()
        {
            return new List<Server>
    {
        new Server
        {
            Hostname = "Server1",
            Map = "Map1",
            Players = 20,
            MaxPlayers = 50,
            Tags = "PvP, Custom",
            Ip = "192.168.1.1",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the first test server.",
            HeaderImage = "https://example.com/image1.png",
            Status = "vip",
            URL = new List<URL>
            {
                new URL { VK = "https://vk.com/server1", Discord = "https://discord.gg/server1" }
            }
        },
        new Server
        {
            Hostname = "Server2",
            Map = "Map2",
            Players = 10,
            MaxPlayers = 40,
            Tags = "PvE",
            Ip = "192.168.1.2",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the second test server.",
            HeaderImage = "https://example.com/image2.png",
            Status = "vip",
            URL = new List<URL>
            {
                new URL { Telegram = "https://t.me/server2", Steam = "https://steamcommunity.com/server2" }
            }
        },
        new Server
        {
            Hostname = "Server3",
            Map = "Map3",
            Players = 5,
            MaxPlayers = 30,
            Tags = "Survival",
            Ip = "192.168.1.3",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the third test server.",
            HeaderImage = "https://example.com/image3.png",
            Status = "vip",
            URL = new List<URL>
            {
                new URL { Site = "https://server3.com" }
            }
        },
        new Server
        {
            Hostname = "Server4",
            Map = "Map4",
            Players = 25,
            MaxPlayers = 50,
            Tags = "Roleplay",
            Ip = "192.168.1.4",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the fourth test server.",
            HeaderImage = "https://example.com/image4.png",
            Status = "premium",
            URL = new List<URL>
            {
                new URL { VK = "https://vk.com/server4", Discord = "https://discord.gg/server4", Telegram = "https://t.me/server4" }
            }
        },
        new Server
        {
            Hostname = "Server5",
            Map = "Map5",
            Players = 15,
            MaxPlayers = 60,
            Tags = "PvP",
            Ip = "192.168.1.5",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the fifth test server.",
            HeaderImage = "https://example.com/image5.png",
            Status = "vip",
            URL = new List<URL>
            {
                new URL { VK = "https://vk.com/server5" }
            }
        },
        new Server
        {
            Hostname = "Server6",
            Map = "Map6",
            Players = 35,
            MaxPlayers = 50,
            Tags = "Custom",
            Ip = "192.168.1.6",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the sixth test server.",
            HeaderImage = "https://example.com/image6.png",
            Status = "premium",
            URL = new List<URL>
            {
                new URL { Steam = "https://steamcommunity.com/server6", Site = "https://server6.com" }
            }
        },
        new Server
        {
            Hostname = "Server7",
            Map = "Map7",
            Players = 10,
            MaxPlayers = 45,
            Tags = "PvP, Hardcore",
            Ip = "192.168.1.7",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the seventh test server.",
            HeaderImage = "https://example.com/image7.png",
            Status = "vip",
            URL = new List<URL>
            {
                new URL { Discord = "https://discord.gg/server7" }
            }
        },
        new Server
        {
            Hostname = "Server8",
            Map = "Map8",
            Players = 50,
            MaxPlayers = 100,
            Tags = "PvE, Custom",
            Ip = "192.168.1.8",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the eighth test server.",
            HeaderImage = "https://example.com/image8.png",
            Status = "vip",
            URL = new List<URL>
            {
                new URL { VK = "https://vk.com/server8", Site = "https://server8.com" }
            }
        },
        new Server
        {
            Hostname = "Server9",
            Map = "Map9",
            Players = 5,
            MaxPlayers = 25,
            Tags = "Roleplay, Custom",
            Ip = "192.168.1.9",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the ninth test server.",
            HeaderImage = "https://example.com/image9.png",
            Status = "vip",
            URL = new List<URL>
            {
                new URL { Telegram = "https://t.me/server9" }
            }
        },
        new Server
        {
            Hostname = "Server10",
            Map = "Map10",
            Players = 20,
            MaxPlayers = 40,
            Tags = "PvP, Custom",
            Ip = "192.168.1.10",
            Port = 27015,
            QueryPort = 27016,
            Description = "This is the tenth test server.",
            HeaderImage = "https://example.com/image10.png",
            Status = "premium",
            URL = new List<URL>
            {
                new URL { VK = "https://vk.com/server10", Steam = "https://steamcommunity.com/server10" }
            }
        }
    };
        }

    }
}
