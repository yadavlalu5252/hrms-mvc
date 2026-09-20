using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IDesignationService
    {
        Task<List<Designation>> GetAllDesignation();

        Task<Designation?> GetDesignationById(int id);

        Task<int> AddDesignation(Designation designation);

        Task<int> UpdateDesignation(Designation designation);

        Task<int> DeleteDesignation(int id);
    }
}
