using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAttendanceByUserId(int userId);

        Task<List<Attendance>> GetAttendanceList();

        Task<Attendance?> GetAttendanceById(int id);

        Task<List<Attendance>> GetAttendanceByDate(
            DateTime? startDate,
            DateTime? endDate);

        Task AddAttendance(Attendance attendance);

        Task UpdateAttendance(Attendance attendance);

        Task DeleteAttendance(int id);
        Task<List<User>> GetEmployees();
    }
}