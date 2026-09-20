using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IDailyReportService
    {
        Task<int> TotalPresent();
        Task<int> TotalAbsent();

        Task<int> CompletedTasks();
        Task<int> PendingTasks();

        Task<List<Attendance>> GetDailyAttendance( string? status,string? sortBy);
    }
}