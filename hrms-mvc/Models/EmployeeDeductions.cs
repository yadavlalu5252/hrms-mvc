using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class EmployeeDeductions
    {
        
        public int Id { get; set; }

        [ForeignKey("EmployeeSalaries")]
        public int SalaryId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Deduction")]
        public int DeductionId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DeductionAmount { get; set; }

        public virtual EmployeeSalaries EmployeeSalaries { get; set; }
        public virtual Deduction Deduction { get; set; }
    }
}
