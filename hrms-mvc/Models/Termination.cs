using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Termination
    {
        [Key]
        public int TerminationId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string? TerminationType { get; set; }

        [Required]
        public DateTime NoticeDate { get; set; }

        [Required]
        public DateTime ResignDate { get; set; }

        [Required]
        [StringLength(300)]
        public string? Reason { get; set; }

        
        public User? User { get; set; }
    }
}
