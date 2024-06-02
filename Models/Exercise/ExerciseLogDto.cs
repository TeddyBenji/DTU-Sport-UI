namespace DTU_Sport_UI.Models
{
    public class ExerciseLogDto
{

    public string UserName { get; set; }
    public DateTime Date { get; set; }
    public string Exercise { get; set; }
    public List<ExerciseMetricDto> Metrics { get; set; }

        public TrainingLogDto ToTrainingLogDto()
        {
            return new TrainingLogDto
            {
                UserName = this.UserName,
                ExerciseName = this.Exercise,
                ExerciseDate = this.Date,
                Metrics = this.Metrics.Select(m => new TrainingMetricDto { Name = m.MetricName, Value = m.Value }).ToList()
            };
        }

    }
}
