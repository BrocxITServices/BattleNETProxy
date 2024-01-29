using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using static System.Formats.Asn1.AsnWriter;


internal class Program
{
    const string clientId = "";
    const string clientSecret = "";
    private static async Task Main(string[] args)
    {
        var ServerToken = await GetGameToken();
        Console.WriteLine(ServerToken);
        string GameToken = ServerToken;
        var AccessToken = await GetUserToken();
        Console.WriteLine(AccessToken);
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
    static async Task<string> GetUserToken()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://oauth.battle.net/");
        var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);

        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("redirect_uri", "https://localhost:7141/"),
            new KeyValuePair<string, string>("scope", "wow.profile"),
            new KeyValuePair<string, string>("state" , "ldiksauhadshlskadhlsadkhjsldakj"),
        }) ;


        var response = await client.PostAsync("token", formData);
        //if (!response.IsSuccessStatusCode) throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");

        var gameToken = (await response.Content.ReadFromJsonAsync<GameToken>()) ?? throw new NullReferenceException();
        return gameToken.access_token ?? throw new NullReferenceException();
    }
}


public record class GameToken(
    string access_token,
    string token_type,
    int expires_in
    );
public record class ABC_Response(
    string access_token,
    string token_type,
    int expires_in
    );



