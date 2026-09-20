using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class EmployeeDocumentUploadViewModel
    {
        [Required(ErrorMessage = "Document Name is compulsory")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Please select a file")]
        public IFormFile? File { get; set; }
    }
}