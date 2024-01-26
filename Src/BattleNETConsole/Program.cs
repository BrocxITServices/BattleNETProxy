using System.Net.Http.Headers;

internal class Program
{
    const string clientId= "";
    const string clientSecret = "";
    private static void Main(string[] args)
    {
        var token = GetGameToken();

        Console.WriteLine("Hello, World!");
    }

    private static string GetGameToken()
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri("https://oauth.battle.net/authorize");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Basic");
        // now make the value for the authorization header clientid:clientsecret

        var content = new FormUrlEncodedContent(new[]
        {
        new KeyValuePair<string, string>("grant_type", "client_credentials")
        });



        throw new NotImplementedException();
    }
}