using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Documents
    {
        [Key]
        public int DocId { get; set; }
        [Required(ErrorMessage = "DocumentName is compulsory")]
        public string DocName { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "DocumentFile is compulsory")]
        public string DocFile { get; set; }
    }
}
