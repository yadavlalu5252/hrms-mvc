using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Designation
    {

        public int Id { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        [Required(ErrorMessage = "Designation Name is required.")]
        public string? DesignationName { get; set; }
        public int? NoOfEmployee { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

        //public List<Earning>? Earnings { get; set; }
        //public List<Deduction>? Deductions { get; set; }
    }
}