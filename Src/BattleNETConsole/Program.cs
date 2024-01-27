using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;


internal class Program
{
    const string clientId = "";
    const string clientSecret = "";
    private static async Task Main(string[] args)
    {
        var ServerToken = await GetGameToken();
        Console.WriteLine(ServerToken);
        string GameToken = ServerToken;
    }
    private static async Task<string> GetGameToken()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://oauth.battle.net/");

        // clientId and clientSecret
        var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

        // form data
        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

        // Make the POST request
        var response = await client.PostAsync("token", formData);
        if (!response.IsSuccessStatusCode) throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");

        var abc_responseObject = (await response.Content.ReadFromJsonAsync<ABC_Response>()) ?? throw new NullReferenceException();
        return abc_responseObject.access_token ?? throw new NullReferenceException();
    }
    public record class ABC_Response(
    string access_token,
    string token_type,
    int expires_in
    );


}
