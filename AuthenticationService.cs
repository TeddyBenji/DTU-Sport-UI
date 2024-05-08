using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Blazored.SessionStorage;

public class AuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ISessionStorageService _sessionStorage;

    public AuthenticationService(HttpClient httpClient, ISessionStorageService sessionStorage)
    {
        _httpClient = httpClient;
        _sessionStorage = sessionStorage;
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var requestBody = new Dictionary<string, string>
        {
            {"grant_type", "password"},
            {"username", username},
            {"password", password},
            {"client_id", "SportUI"},
            {"client_secret", "DtuSport"},
            {"scope", "openid profile read write update Delete"}
        };

        var response = await _httpClient.PostAsync("http://localhost:5250/connect/token", new FormUrlEncodedContent(requestBody));
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("JSON Response: " + jsonResponse);
            var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();
            if (authResult != null && !string.IsNullOrEmpty(authResult.Token))
            {
                authResult.IsSuccess = true;
                await _sessionStorage.SetItemAsync("authToken", authResult.Token);
                await _sessionStorage.SetItemAsync("tokenExpiry", DateTime.UtcNow.AddSeconds(authResult.ExpiresIn));
                return authResult;
            }
            else
            {
                return new AuthResult { IsSuccess = false, Error = "Invalid response format." };
            }
        }
        else
        {
            return new AuthResult { IsSuccess = false, Error = "Authentication failed." };
        }
    }

    public async Task LogoutAsync()
    {
        await _sessionStorage.RemoveItemAsync("authToken");
        await _sessionStorage.RemoveItemAsync("tokenExpiry");
    }

    public class AuthResult
    {
        [JsonPropertyName("access_token")]  
        public string Token { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        public bool IsSuccess { get; set; }  // This doesn't come from JSON, you set it manually
        public string Error { get; set; }
    }
}




