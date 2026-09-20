using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ITrainingListService
    {
        Task<List<Traininglist>> FetchAllTrainingLists();
        Task<TrainingListModal> FindTrainingListByID(int id);
        Task AddTrainingList(TrainingListModal trainingList);
        Task UpdateTrainingList(TrainingListModal trainingList);
        Task DeleteTrainingList(int id);
    }
}