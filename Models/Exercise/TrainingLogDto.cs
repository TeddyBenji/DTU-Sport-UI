using Microsoft.Security.Application;

namespace DTU_Sport_UI.Models
{
    public class TrainingLogDto
    {
        public string UserName { get; set; }
        public string ExerciseName { get; set; }
        public DateTime ExerciseDate { get; set; }
        public List<TrainingMetricDto> Metrics { get; set; }

        // Sanitization before sending it
        public void Sanitize()
        {
            UserName = Encoder.HtmlEncode(UserName);
            ExerciseName = Encoder.HtmlEncode(ExerciseName);
        }

        public ExerciseLogDto ToExerciseLogDto()
        {
            return new ExerciseLogDto
            {
                UserName = this.UserName,
                Date = this.ExerciseDate,
                Exercise = this.ExerciseName,
            };
        }
    }
}



