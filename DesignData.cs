using System.Collections.ObjectModel;

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
            LoadServers();
            LoadNews();
            LoadHosts();
        }

        private void LoadServers()
        {

            Servers = new ObservableCollection<Server>
            {
                new Server { Name = "StaticRust.ru", Description = "This is the first server", PlayerOnline = 20, ServerType = ServerType.Premium, ServerCategory = "КЛАССИКА", Country = "МОСКОВСКИЙ", Status = "Online", Version = 1, MapName = "Map A" },
                new Server { Name = "Server 2", Description = "This is the second server", PlayerOnline = 15, ServerType = ServerType.Vip, ServerCategory = "Classic", Country = "UK", Status = "Offline", Version = 2, MapName = "Map B" },
                new Server { Name = "Server 3", Description = "This is the third server", PlayerOnline = 30, ServerType = ServerType.Premium, ServerCategory = "Classic", Country = "Germany", Status = "Online", Version = 3, MapName = "Map C" },
                new Server { Name = "Server 4", Description = "This is the fourth server", PlayerOnline = 25, ServerType = null, ServerCategory = "Classic", Country = "France", Status = "Maintenance", Version = 4, MapName = "Map D" }
            };
        }

        private void LoadNews()
        {
            NewsCollection = new ObservableCollection<NewsItem>
        {
            new NewsItem
            {
                Title = "Новость 1",
                Description = "Описание первой новости",
                ReleaseDate = DateTime.Now.AddDays(-1)
            },
            new NewsItem
            {
                Title = "Новость 2",
                Description = "Описание второй новости",
                ReleaseDate = DateTime.Now.AddDays(-2)
            },
            new NewsItem
            {
                Title = "Новость 3",
                Description = "Описание третьей новости",
                ReleaseDate = DateTime.Now.AddDays(-3)
            },
            new NewsItem
            {
                Title = "Новость 4",
                Description = "Описание четвертой новости",
                ReleaseDate = DateTime.Now.AddDays(-4)
            },
            new NewsItem
            {
                Title = "Новость 5",
                Description = "Описание пятой новости",
                ReleaseDate = DateTime.Now.AddDays(-5)
            }
        };
        }

        private void LoadHosts()
        {
            Hosts = new ObservableCollection<Hosting>
        {
            new Hosting
            {
                Name = "HostOne",
                Description = "A reliable hosting service with excellent uptime.",
                Users = 1500,
                Projects = 350,
                HostingType = HostingType.Reliable
            },
            new Hosting
            {
                Name = "HostTwo",
                Description = "Our choice for budget-friendly hosting.",
                Users = 2300,
                Projects = 500,
                HostingType = HostingType.OurChoice
            },
            new Hosting
            {
                Name = "HostThree",
                Description = "Premium hosting with advanced features.",
                Users = 1200,
                Projects = 200,
                HostingType = HostingType.Reliable
            },
            new Hosting
            {
                Name = "HostFour",
                Description = "Affordable and dependable hosting for small businesses.",
                Users = 1800,
                Projects = 450,
                HostingType = HostingType.OurChoice
            }
        };
        }
    }
}
