using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string? DesignationFrom { get; set; }

        [Required]
        [StringLength(100)]
        public string? DesignationTo { get; set; }

        [Required]
        public DateTime Date {  get; set; }

        public User? User { get; set; }

    }
}
