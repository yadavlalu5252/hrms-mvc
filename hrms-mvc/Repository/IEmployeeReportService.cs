using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IEmployeeReportService
    {
        Task<int> GetTotalEmployees();
        Task<int> GetTotalDepartments();
        Task<int> GetActiveEmployees();
        Task<int> GetTotalRoles();

        Task<List<User>> GetEmployees(string? status,string? dateFilter);
    }
}