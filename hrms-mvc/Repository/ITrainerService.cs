using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ITrainerService
    {
        Task<List<Trainers>> FetchAllTrainers();
        Task<Trainers> FindTrainersByID(int id);
        Task AddTrainers(Trainers trainer);
        Task UpdateTrainers(Trainers trainer);
        Task DeleteTrainers(int id);
    }
}