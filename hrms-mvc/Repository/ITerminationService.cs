using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ITerminationService
    {
        Task<List<Termination>> GetAllTerminations();
        Task<Termination?> GetTerminationById(int id);
        Task<int> AddTermination(Termination termination);
        Task<int> UpdateTermination(Termination termination);
        Task<int> DeleteTermination(int id);
        Task<List<User>> GetAllUsers();
    }
}
