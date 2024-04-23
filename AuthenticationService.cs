using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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
        var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();
        return authResult ?? new AuthResult { IsSuccess = false, Error = "Invalid response from server" };
        }   
        return new AuthResult { IsSuccess = false, Error = "Authentication failed." };

}

public class AuthResult
{
    public bool IsSuccess { get; set; }
    public string Error { get; set; }
    public string Token { get; set; }
}
}


