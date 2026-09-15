using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Traininglist
    {
        [Key]
        public int TrainingId { get; set; }

        [ForeignKey("Trainers")]
        public int TrainersId { get; set; }
        public Trainers Trainers { get; set; }
        [ForeignKey("TrainingType")]
        public int TrainingTypeId { get; set; }
        public TrainingType TrainingType { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "TrainingCost is compulsory")]
        [Column(TypeName = "decimal(14, 4)")]
        public decimal TrainingCost { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string ModifiedBy { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }

        public string? ProfilePicture { get; set; }

        public List<string> images { get; set; } = new List<string>();

        
    }
}
