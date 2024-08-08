namespace StaticRustLauncher.Services.Base;

public class DataService<T> : IDataService<T>
{
    private readonly HttpClient _httpClient;
    public DataService(HttpClient httpClient) =>
            _httpClient = httpClient;

    public async Task<List<T>> GetDataAsync(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var jsonData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var dataCollection = JsonSerializer.Deserialize<DataCollection<T>>(jsonData, options);

        return dataCollection?.Items ?? [];
    }
}
