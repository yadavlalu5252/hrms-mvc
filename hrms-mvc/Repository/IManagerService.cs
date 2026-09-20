using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IManagerService
    {
        Task<User?> GetManager(int userId);

        Task<decimal> GetTotalWorkingHours(int userId, DateTime date);

        Task<decimal> GetProductiveHours(int userId, DateTime date);

        Task<decimal> GetBreakHours(int userId, DateTime date);

        Task<decimal> GetOvertimeHours(int userId, DateTime date);

        Task<List<ManagerProjectViewModel>> GetManagerProjects(int userId);

        Task<List<ManagerTaskViewModel>> GetManagerTasks(int userId);
    }
}