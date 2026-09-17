using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ILeaveReportService
    {
        Task<int> TotalLeaves();
        Task<int> ApprovedLeaves();
        Task<int> PendingLeaves();
        Task<int> RejectedLeaves();

        Task<List<LeaveRequest>> GetLeaveRecords( string? dateFilter,string? Status,String? SortBy);

    }
}
