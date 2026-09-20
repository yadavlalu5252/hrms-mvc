using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IResignationService
    {
        Task<List<Resignation>> GetAllResignations();
        Task<Resignation?> GetResignationById(int id);
        Task<int> AddResignation(Resignation resignation);
        Task<int> UpdateResignation(Resignation resignation);
        Task<int> DeleteResignation(int id);
        Task<List<User>> GetAllUsers();
        Task<List<Department>> GetAllDepartments();
    }
}
