using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class TrainingListModal
    {
        [Key]
        public int TrainingId { get; set; }  

        [Required]
        public int TrainersId { get; set; }

        [Required]
        public int TrainingTypeId { get; set; }

        [Required(ErrorMessage = "UserId is compulsory")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "TrainingCost is compulsory")]
        [Column(TypeName = "decimal(14, 4)")]
        public decimal TrainingCost { get; set; }
        [Required(ErrorMessage = "Description is compulsory")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is compulsory")]
        public string Status { get; set; }

        [Required(ErrorMessage = "StartDate is compulsory")]

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is compulsory")]

        public DateTime EndDate { get; set; }
    }
}
