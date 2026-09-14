using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Resignation
    {
        [Key]
        public int ResignationId { get; set; }

        [Required(ErrorMessage = "Employee is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Notice date is required")]
        [DataType(DataType.Date)]
        public DateTime NoticeDate { get; set; }

        [Required(ErrorMessage = "Resign date is required")]
        [DataType(DataType.Date)]
        public DateTime ResignDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(300, ErrorMessage = "Reason must be between 10 and 300 characters", MinimumLength = 10)]
        public string? Reason { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}
