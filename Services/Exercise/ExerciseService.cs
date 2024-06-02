// Services/ExerciseService.cs
using DTU_Sport_UI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTU_Sport_UI.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly HttpClient _httpClient;

        public ExerciseService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServerAPI");
        }

        public async Task<List<ExerciseLogDto>> GetWorkoutLogsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ExerciseLogDto>>("https://localhost:7158/api/Exercise/GetWorkoutLogs");
            if (response != null)
            {
                return response;
            }
            else
            {
                throw new HttpRequestException("Failed to load workout logs or none exist.");
            }
        }

        
       
        public async Task<List<ExerciseModel>> GetAllExercisesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ExerciseModel>>("https://localhost:7158/api/Exercise/Get/exercises");
        }

        public async Task<bool> RegisterTrainingAsync(TrainingLogDto logDto)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7158/api/Exercise/RegisterTraining", logDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<MetricDto>> GetAllMetricAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MetricDto>>("https://localhost:7158/api/Exercise/Get/Metric");
        }

    }
}

