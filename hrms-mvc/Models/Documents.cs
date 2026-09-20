using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Documents
    {
        [Key]
        public int DocId { get; set; }

        [Required(ErrorMessage = "Email is compulsory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "DocumentName is compulsory")]
        public string DocName { get; set; }

        [Required(ErrorMessage = "DocumentFile is compulsory")]
        public string DocFile { get; set; }

        [NotMapped]
        public IFormFile? DocumentFile { get; set; }
    }
}