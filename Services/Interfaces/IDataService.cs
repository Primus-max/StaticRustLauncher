namespace StaticRustLauncher.Services.Interfaces;

public interface  IDataService<T>
{
    Task<List<T>> GetDataAsync(string endpoint);
}
