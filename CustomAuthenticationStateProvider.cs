using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userName = await _localStorage.GetItemAsStringAsync("username");
        
        if (string.IsNullOrEmpty(userName))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userName),
        }, "apiauth_type");

        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(string username)
    {
        await _localStorage.SetItemAsStringAsync("username", username);
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, username),
        }, "apiauth_type");

        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.RemoveItemAsync("username");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }
}

