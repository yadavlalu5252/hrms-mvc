using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRole();

        Task<Role?> GetRoleById(int id);

        Task<int> AddRole(Role role);

        Task<int> UpdateRole(Role role);

        Task<int> DeleteRole(int id);
    }
}
