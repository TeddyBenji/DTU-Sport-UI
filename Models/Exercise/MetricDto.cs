using System.ComponentModel.DataAnnotations;

namespace DTU_Sport_UI.Models
{
    public class MetricDto
{
        [Key]
        public int MetricID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }



    }
}
