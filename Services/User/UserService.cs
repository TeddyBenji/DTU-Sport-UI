using DTU_Sport_UI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace DTU_Sport_UI.Services
{
    public class UserService : IUserService 
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServerAPI");
        }

        
        public async Task<string> GetBioAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7158/api/User/GetBio"); 
                if (response.IsSuccessStatusCode)
                {
                    
                    var bio = await response.Content.ReadAsStringAsync();
                    return bio;
                }
                return "Bio not available";  
            }
            catch (Exception ex)
            {
               
                return $"Failed to load bio: {ex.Message}";
            }
        }

        public async Task<bool> UpdateBioAsync(BioModel bioModel)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(bioModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7158/api/User/UpdateBio", jsonContent);
            return response.IsSuccessStatusCode;
        }


        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UserDto>>("https://localhost:7158/api/User/GetAllUsers");
        }

        public async Task<bool> AssignRoleAsync(AssignRoleModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7239/user/Role/AssignRole", model);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteUserAsync(string username)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7239/Dtu/User/{username}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteUserAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SignUpAsync(SignUpModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7239/Dtu/User/register", model);
            return response.IsSuccessStatusCode;
        }



    }
}

