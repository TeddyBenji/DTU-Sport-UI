using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using System.IdentityModel.Tokens.Jwt;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ISessionStorageService _sessionStorage;

    public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _sessionStorage.GetItemAsStringAsync("authToken");
        if (!string.IsNullOrEmpty(token))
        {
            token = token.Trim('"'); // Trim the quotation marks

            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (handler.CanReadToken(token))
                {
                    var jwt = handler.ReadJwtToken(token);
                    var username = jwt.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

                    if (username != null)
                    {
                        var identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, username),
                        }, "apiauth_type");

                        var user = new ClaimsPrincipal(identity);
                        return new AuthenticationState(user);
                    }
                }
                else
                {
                    Console.WriteLine("Cannot read token, invalid format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JWT: {ex.Message}");
            }
        }
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
}
