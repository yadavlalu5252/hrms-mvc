using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class TrainingType
    {
        [Key]
        public int TrainingTypeId { get; set; }

        [Required (ErrorMessage = "TrainingTypeName is compulsory")]
        public string TrainingTypeName { get; set; }

        [Required(ErrorMessage = "Description is compulsory")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is compulsory")]
        public string Status { get; set; }
    }
}
 