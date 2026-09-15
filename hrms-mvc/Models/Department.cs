using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class Department
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Department Name is required.")]
        public string? Name { get; set; }
        public int? NoOfEmployee { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<Designation>? Designations { get; set; }

        //public List<DepartmentLeaves> DepartmentLeaves { get; set; }

        //public List<Earning> Earnings { get; set; }
        //public List<Deduction> Deductions { get; set; }
    }
}