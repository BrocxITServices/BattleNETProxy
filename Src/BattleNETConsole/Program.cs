using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;


internal class Program
{
    const string clientId = "cd59888c589a4f3da5ba27c37a8cd55d";
    const string clientSecret = "KsJCjem7LfNXKa7d4XhmPwpgZETvmdx5";
    private static async Task Main(string[] args)
    {
        var ServerToken = await GetGameToken();
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

        var abc_responseObject = (await response.Content.ReadFromJsonAsync<dynamic>()) ?? throw new NullReferenceException();
        return abc_responseObject.access_token ?? throw new NullReferenceException();
    }


}
