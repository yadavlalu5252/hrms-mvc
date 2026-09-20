using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class MasterLeaveType
    {
        
        public int Id { get; set; }
        public string Leave{ get; set; }
        public string Status { get; set; }="Active";
        public List<DepartmentLeaves> DepartmentLeaves { get; set; }
        public List<LeaveBalance> LeaveBalances { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
