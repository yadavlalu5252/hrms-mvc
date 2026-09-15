using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class DeductionType
    {
        
        public int Id { get; set; } 

        [Required]
        [StringLength(100)]
        public string DeductionsName { get; set; }

        public virtual ICollection<Deduction> Deductions { get; set; }
    }
}
