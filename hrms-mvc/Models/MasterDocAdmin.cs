using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class MasterDocAdmin
    {
        [Key]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "DocumentName is required")]
        public string AdminDocName { get; set; }
    }
}
