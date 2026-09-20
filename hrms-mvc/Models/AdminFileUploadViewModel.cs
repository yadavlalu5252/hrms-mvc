using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class AdminFileUploadViewModel
    {
        [Required(ErrorMessage = "Email is compulsory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "DocumentName is compulsory")]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "Please select a file")]
        public IFormFile? File { get; set; }
    }
}