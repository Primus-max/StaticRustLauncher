namespace StaticRustLauncher.Services.Base;

public class GenericService<T> : IGenericService<T>
{
    public readonly HttpClient _httpClient = null!;

    public GenericService(HttpClient httpClient)
        => _httpClient = httpClient;

    public async Task<IEnumerable<T>> GetDataAsync(string url)
    {
        var response = await _httpClient.GetStringAsync(url);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<T>>(response) ?? Enumerable.Empty<T>();
    }
}
