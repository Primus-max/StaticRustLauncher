using Newtonsoft.Json;

namespace StaticRustLauncher.Transport;

public static class SFTPClient
{
    public static async Task<SftpClient> GetAsync()
    {
        string apiUrl = "http://194.147.90.218/launcher/version";
        using var httpClient = new HttpClient();

        try
        {
            var response = await httpClient.GetStringAsync(apiUrl);
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            if (data.TryGetValue("SFTP", out var sftpData))
            {
                var sftpArray = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(sftpData.ToString());
                var sftpInfo = sftpArray?.FirstOrDefault();

                if (sftpInfo != null)
                {
                    string host = sftpInfo.TryGetValue("host", out var hostValue) ? hostValue.ToString() : "";
                    int port = sftpInfo.TryGetValue("port", out var portValue) ? Convert.ToInt32(portValue) : 22;
                    string user = sftpInfo.TryGetValue("user", out var userValue) ? userValue.ToString() : "";
                    string pass = sftpInfo.TryGetValue("pass", out var passValue) ? passValue.ToString() : "";

                    return new SftpClient(host, port, user, pass);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении данных SFTP: {ex.Message}");
        }

        return null; // Или выбросите исключение, если это более предпочтительно
    }
}

