using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DTU_Sport_UI.Models;

namespace DTU_Sport_UI.Services
{
    public class ClubService : IClubService
    {
        private readonly HttpClient _httpClient;

        public ClubService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServerAPI");
        }

        public async Task<List<ClubDto>> GetAllClubsAsync()
{
    var response = await _httpClient.GetAsync("https://localhost:7158/api/Clubs/RetrivAllClubs");
    if (response.IsSuccessStatusCode)
    {
        return await response.Content.ReadFromJsonAsync<List<ClubDto>>();
    }
    else
    {
        
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new InvalidOperationException($"API call failed: {response.StatusCode}\n{errorContent}");
    }
}

        public async Task<bool> RegisterMemberAsync(string clubName, string username)
        {
            var registrationInfo = new { Username = username, ClubName = clubName };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7158/api/Clubs/RegisterMember", registrationInfo);
            return response.IsSuccessStatusCode;
        }

        public async Task<ClubModel> GetClubDetailsAsync(string clubName)
        {
            return await _httpClient.GetFromJsonAsync<ClubModel>($"https://localhost:7158/api/Clubs/ClubPage/{clubName}");
        }

        public async Task<bool> CreateEventAsync(EventModel eventModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7158/api/Clubs/CreateEvent", eventModel);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating event: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteClubAsync(string clubName)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7158/api/Clubs/DeleteClub/{clubName}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting club: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateClubAsync(ClubModel newClub)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7158/api/Clubs/create", newClub);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating club: {ex.Message}");
                return false;
            }
        }

    }
}

