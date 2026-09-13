using hrms_mvc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class LeaveBalance
    {
        [Key]
        public int LeaveBalanceId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; }

        [ForeignKey("DepartmentLeaves")]
        public int DepartmentLeavesId { get; set; }

        public DepartmentLeaves DepartmentLeaves { get; set; }

        public int TotalLeaves { get; set; }

        public int UsedLeaves { get; set; }

        [NotMapped]
        public int RemainingLeaves => TotalLeaves - UsedLeaves;

        public void ApplyLeave(int numberOfDays)
        {
            if (RemainingLeaves < numberOfDays)
            {
                throw new InvalidOperationException(
                    "Insufficient leave balance.");
            }

            UsedLeaves += numberOfDays;
        }

        public void RevertLeave(int numberOfDays)
        {
            UsedLeaves -= numberOfDays;

            if (UsedLeaves < 0)
            {
                UsedLeaves = 0;
            }
        }
    }
}