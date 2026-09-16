using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IAttendanceReportService
    {
        Task<int> TotalWorkingDays();

        Task<int> TotalLeaveTaken();

        Task<int> TotalHolidays();

        Task<int> TotalHalfDays();

        Task<List<Attendance>> GetAttendanceRecords(string? dateFilter,string? status,string? sortBy);
    }
}