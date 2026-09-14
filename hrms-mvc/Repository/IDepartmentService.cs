using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartments();

        Task<Department?> GetDepartmentById(int id);

        Task<int> AddDepartment(Department department);

        Task<int> UpdateDepartment(Department department);

        Task<int> DeleteDepartment(int id);
    }
}
