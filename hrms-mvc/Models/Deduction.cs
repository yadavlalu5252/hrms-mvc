using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Deduction
    {
        
        public int Id { get; set; } 

        [ForeignKey("DeductionType")]
        public int DeductionTypeId { get; set; }

        public int DepartmentId { get; set; }

        public int DesignationId { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal DeductionPercent { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; } 

        public DateTime? ModifiedAt { get; set; }

        public string? ModifiedBy { get; set; } 
        public virtual DeductionType DeductionType { get; set; }
        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
    }
}
