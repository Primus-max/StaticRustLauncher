using System.Collections.ObjectModel;

namespace StaticRustLauncher.DesignData
{
    /// <summary>
    /// Класс с тестовыми наборами данных (Mock), если не надо, можно удалить
    /// </summary>
    public class DesignData
    {
        public ObservableCollection<Server> Servers { get; set; } = new ObservableCollection<Server>();
        public ObservableCollection<NewsItem> NewsCollection { get; set; } = [];
        public ObservableCollection<Hosting> Hosts { get; set; } = [];

        public DesignData()
        {
            GenerateTestServers();
            //LoadNews();
            //LoadHosts();
        }

        public  ObservableCollection<Server> GenerateTestServers()
        {
            var random = new Random();
            var statuses = new[] { "vip", "premium" };

            var servers = new ObservableCollection<Server>();

            for (int i = 1; i <= 10; i++)
            {
                var status = statuses[random.Next(statuses.Length)];

                servers.Add(new Server
                {
                    Hostname = $"Server {i}",
                    Map = $"Map {random.Next(1, 5)}",
                    Players = random.Next(1, 100),
                    MaxPlayers = random.Next(100, 200),
                    Tags = $"Tag {random.Next(1, 5)}",
                    Ip = $"192.168.1.{random.Next(1, 255)}",
                    Port = random.Next(1000, 65535),
                    QueryPort = random.Next(1000, 65535),
                    Description = $"Description for server {i}",
                    HeaderImage = $"http://example.com/image{i}.jpg",
                    Status = status,
                    URL = new List<URL>
                {
                    new URL
                    {
                        VK = $"http://vk.com/server{i}",
                        Discord = $"http://discord.gg/server{i}",
                        Telegram = $"http://t.me/server{i}",
                        Steam = $"http://steamcommunity.com/server{i}",
                        Site = $"http://server{i}.com"
                    }
                }
                });
            }

            return servers;
        }


        public void GenerateTestHosts()
        {
            var random = new Random();
            var hostingTypes = Enum.GetValues(typeof(HostingType));

            for (int i = 1; i <= 10; i++)
            {
                var hostingType = (HostingType)hostingTypes.GetValue(random.Next(hostingTypes.Length));

                Hosts.Add(new Hosting
                {
                    Name = $"Hosting {i}",
                    Description = $"Description for hosting {i}",
                    Image = $"http://example.com/image{i}.jpg",
                    Users = random.Next(1, 1000),
                    Projects = random.Next(1, 500),
                    Url = $"http://hosting{i}.com",
                    Status = random.Next(0, 2), // Assuming Status is an int that corresponds to the enum values
                    HostingType = hostingType
                });
            }
        }
    }
}
