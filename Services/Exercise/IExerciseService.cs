using DTU_Sport_UI.Models;

namespace DTU_Sport_UI.Services
{
    public interface IExerciseService
{
        Task<List<ExerciseLogDto>> GetWorkoutLogsAsync();

        Task<List<ExerciseModel>> GetAllExercisesAsync();

        Task<bool> RegisterTrainingAsync(TrainingLogDto logDto);

        Task<List<MetricDto>> GetAllMetricAsync();

    }
}
