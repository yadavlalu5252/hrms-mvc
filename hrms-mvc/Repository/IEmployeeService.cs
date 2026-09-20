using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IEmployeeService
    {
        Task<User?> GetEmployee(int userId);

        Task<decimal> GetTotalWorkingHours(int userId, DateTime date);

        Task<decimal> GetProductiveHours(int userId, DateTime date);

        Task<decimal> GetBreakHours(int userId, DateTime date);

        Task<decimal> GetOvertimeHours(int userId, DateTime date);

        Task<List<EmployeeProjectViewModel>> GetEmployeeProjects(int userId);

        Task<List<EmployeeTaskViewModel>> GetEmployeeTasks(int userId);
    }
}