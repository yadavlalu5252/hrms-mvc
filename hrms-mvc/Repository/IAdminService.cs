using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IAdminService
    {
        Task<int> GetTotalEmployees();

        Task<int> GetPresentEmployees(DateTime date);

        Task<int> GetHalfDayEmployees(DateTime date);

        Task<int> GetAbsentEmployees(DateTime date);

        Task<int> GetTotalClients();

        Task<int> GetTotalTasks();

        Task<decimal> GetTotalEarnings();

        Task<int> GetNewHires();
        Task<int> GetTotalProjects();

        Task<decimal> GetProductionHours();

        Task<decimal> GetWorkingHours();

        Task<decimal> GetBreakHours();

        Task<int> GetCompletedTasks();

        Task<int> GetOnHoldTasks();

        Task<int> GetInProgressTasks();

        Task<int> GetPendingTasks();

        Task<List<DepartmentEmployeeViewModel>> GetEmployeesByDepartment();

        Task<List<AttendanceDashboardViewModel>> GetClockInOutRecords(DateTime date);

        Task<List<EmployeeDashboardViewModel>> GetEmployees();

        Task<List<ProjectDashboardViewModel>> GetProjects();

        Task<List<TaskStatisticsViewModel>> GetTaskStatistics();
    }
}