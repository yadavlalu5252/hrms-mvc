
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class EmployeeSalaries
    {
        
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public virtual User User { get; set; }

        public decimal TotalSalary { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetSalary { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }

  

        public ICollection<EmployeeEarnings> EmployeeEarnings { get; set; }
        public ICollection<EmployeeDeductions> EmployeeDeductions { get; set; }

    }
}
