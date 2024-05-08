using Blazored.SessionStorage;
using System.Net.Http.Headers;

namespace DTU_Sport_UI
{
    public class CustomAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ISessionStorageService _sessionStorage;

        public CustomAuthorizationMessageHandler(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _sessionStorage.GetItemAsStringAsync("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
