using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Earning
    {
        
        public int Id { get; set; }

        [ForeignKey("EarningType")]
        public int EarntypeId { get; set; }
        public EarningType EarningType { get; set; }

        public decimal EarningsPercentage { get; set; }

      
        //[ForeignKey("Department")]
        //public int DepartmentId { get; set; }
        //public Department Department { get; set; }

        //[ForeignKey("Designation")]
        //public int DesignationId { get; set; }
        //public Designation Designation { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

}
