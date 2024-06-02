using System.ComponentModel.DataAnnotations;

namespace DTU_Sport_UI.Models
{
    public class ExerciseModel
{
        [Key]
        public int ExerciseID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

    }
}
