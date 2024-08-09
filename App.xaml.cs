using Microsoft.Extensions.DependencyInjection;

namespace StaticRustLauncher;

/// <summary>
/// Главная точка входа в приложение и конфигурация
/// </summary>
public partial class App : Application
{
    public IServiceProvider? ServiceProvider { get; private set; }
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Настройка Dependency Injection
        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        // Запуск основного окна, передача ServiceProvider в конструктор
        //var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        //mainWindow.Show();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddHttpClient("MainClient", client =>
        {
            client.BaseAddress = new Uri("http://194.147.90.218/launcher");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

    //services.AddScoped<IDataService<NewsCollection>, NewsService>();
    //services.AddScoped<IDataService<ServerCollection>, ServersService>();
    //services.AddScoped<IDataService<ServerCollection>, ServersService>();
    //services.AddScoped<IDataService<HostingCollection>, HostingService>();
    //http://194.147.90.218/launcher/news
    //http://194.147.90.218/launcher/hostings
    //http://194.147.90.218/launcher/serversinfo
        //services.AddTransient<MainWindow>();
    }
}
