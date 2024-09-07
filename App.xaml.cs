

namespace StaticRustLauncher;

/// <summary>
/// Главная точка входа в приложение и конфигурация
/// </summary>
public partial class App : System.Windows.Application
{
    public IServiceProvider? ServiceProvider { get; private set; }
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        SettingsApp.Load();
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
        //services.AddHttpClient("MainClient", client =>
        //{
        //    client.BaseAddress = new Uri("http://194.147.90.218/launcher");
        //    client.DefaultRequestHeaders.Add("Accept", "application/json");
        //});


        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.File(
            path: "logs/log-.txt",        // Путь к файлам логов
            rollingInterval: RollingInterval.Day,  // Ротация файлов ежедневно
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}" // Шаблон вывода
        )
        .CreateLogger();



        //using var loggerFactory = LoggerFactory.Create(builder =>
        //{
        //    builder
        //        .AddFilter("Default", LogLevel.Information)
        //        .AddFile("app.log");
        //});
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
