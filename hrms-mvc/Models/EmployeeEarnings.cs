
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class EmployeeEarnings
    {
       
        public int Id { get; set; }

        [ForeignKey("EmployeeSalaries")]
        public int SalaryId { get; set; }
   

        [ForeignKey("Earning")]
        public int EarningId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal EarningAmount { get; set; }

        public virtual EmployeeSalaries EmployeeSalaries { get; set; }
        public virtual Earning Earning { get; set; }
    }
}
