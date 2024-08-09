using Newtonsoft.Json;

namespace StaticRustLauncher.Services.Base;

public class DataService<T> : IDataService<T>
{
    private readonly HttpClient _httpClient;

    public DataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<T>> GetDataAsync(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var jsonData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        // Deserialize the JSON into a Root<T> object
        var root = JsonConvert.DeserializeObject<Root<T>>(jsonData, options);

        return root?.Items ?? new List<T>();
    }
}