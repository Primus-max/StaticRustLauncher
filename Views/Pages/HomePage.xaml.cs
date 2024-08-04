using System.Collections.ObjectModel;

namespace StaticRustLauncher.Views.Pages;

/// <summary>
/// Логика взаимодействия для HomePage.xaml
/// </summary>
public partial class HomePage : Page
{
    //public ObservableCollection<Server> Servers { get; set; } = [];

    public HomePage()
    {
        InitializeComponent();
        //LoadTestData();
        DataContext = this;
    }

    //private void LoadTestData()
    //{
    //    Servers = new ObservableCollection<Server>
    //        {
    //            new Server { Name = "Server 1", Description = "This is the first server", PlayerCount = 20, Status = "Online" },
    //            new Server { Name = "Server 2", Description = "This is the second server", PlayerCount = 15, Status = "Offline" },
    //            new Server { Name = "Server 3", Description = "This is the third server", PlayerCount = 30, Status = "Online" },
    //            new Server { Name = "Server 4", Description = "This is the fourth server", PlayerCount = 25, Status = "Maintenance" }
    //        };
    //}
}
