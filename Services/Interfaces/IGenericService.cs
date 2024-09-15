namespace StaticRustLauncher.Services.Interfaces;

public interface IGenericService<T>
{
    Task<IEnumerable<T>> GetDataAsync(string url);
}
