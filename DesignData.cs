using System.Collections.ObjectModel;

namespace StaticRustLauncher.DesignData
{
    public class DesignData
    {
        public ObservableCollection<Server> Servers { get; set; }

        public DesignData()
        {
            Servers = new ObservableCollection<Server>
            {
                new Server { Name = "StaticRust.ru", Description = "This is the first server", PlayerOnline = 20, ServerType = ServerType.Premium, ServerCategory = "КЛАССИКА", Country = "МОСКОВСКИЙ", Status = "Online", Version = 1, MapName = "Map A" },
                new Server { Name = "Server 2", Description = "This is the second server", PlayerOnline = 15, ServerType = ServerType.Vip, ServerCategory = "Classic", Country = "UK", Status = "Offline", Version = 2, MapName = "Map B" },
                new Server { Name = "Server 3", Description = "This is the third server", PlayerOnline = 30, ServerType = ServerType.Premium, ServerCategory = "Classic", Country = "Germany", Status = "Online", Version = 3, MapName = "Map C" },
                new Server { Name = "Server 4", Description = "This is the fourth server", PlayerOnline = 25, ServerType = null, ServerCategory = "Classic", Country = "France", Status = "Maintenance", Version = 4, MapName = "Map D" }
            };
        }
    }
}
