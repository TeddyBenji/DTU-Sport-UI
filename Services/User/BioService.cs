using DTU_Sport_UI.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace DTU_Sport_UI.Services
{
    public class BioService : IUserService
{
        private readonly HttpClient _httpClient;

        public BioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BioModel>> UpdateBioAsync(BioModel bioModel)
        {
            // Serialize the bioModel object to JSON
            var jsonContent = new StringContent(JsonSerializer.Serialize(bioModel), Encoding.UTF8, "application/json");

            // Make the PUT request with the serialized bioModel in the request body
            var response = await _httpClient.PutAsync("http://localhost:5115/api/User/UpdateBio", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<BioModel>>();
            }
            else
            {
             
                return null;
            }
        }



    }
}
