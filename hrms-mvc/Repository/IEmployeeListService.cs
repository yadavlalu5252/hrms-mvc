using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IEmployeeListService
    {
        Task<List<User>> GetAllEmployees();
        Task<int> AddEmployee(User user);
        Task<int> UpdateEmployee(User user);
        Task<User?> FindEmployeeById(int id);
        Task<int> DeleteEmployee(int id);
    }
}
