using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class MasterDocEmp
    {
        public int EmpId { get; set; }
        [Required(ErrorMessage = "DocumentName is required.")]
        public string EmpDocName { get; set; }

    }
}
