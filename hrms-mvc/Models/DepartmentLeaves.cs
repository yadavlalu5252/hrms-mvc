using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class DepartmentLeaves
    {

        public int Id { get; set; }

        //[ForeignKey("Department")]
        //public int DepartmentId { get; set; }
        //public Department Department { get; set; }

        [ForeignKey("MasterLeaveType")]
        public int LeaveTypeId { get; set; }
        public MasterLeaveType MasterLeaveType { get; set; }
        
        public int LeavesCount { get; set; }
        public string Status { get; set; }

        public List<LeaveBalance> LeaveBalances { get; set; }
    }
}
