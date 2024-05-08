namespace DTU_Sport_UI.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTU_Sport_UI.Models;

    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServerAPI");
        }

        public async Task<List<NotificationDto>> GetUnreadNotificationsAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5115/api/User/unread-notifications");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<NotificationDto>>();
            }
            else
            {
                // Handle error or return an empty list
                return new List<NotificationDto>();
            }
        }
    }
}

