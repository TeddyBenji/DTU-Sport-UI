using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class AuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
            var jsonResponse = await response.Content.ReadAsStringAsync();  // Read the raw JSON response
            Console.WriteLine(jsonResponse);  // Log it to the console or inspect it in a debugger

            var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();
            if (authResult != null && !string.IsNullOrEmpty(authResult.Token))
            {
                authResult.IsSuccess = true; // Set IsSuccess to true because the token was successfully retrieved
                return authResult;
            }
            else
            {
                return new AuthResult { IsSuccess = false, Error = "Invalid response format: " + jsonResponse };
            }
        }
        else
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            return new AuthResult { IsSuccess = false, Error = "Authentication failed with response: " + errorResponse };
        }
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

        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }

}


