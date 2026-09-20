using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ITrainingTypeService
    {
        Task<List<TrainingType>> FetchAllTrainingTypes();
        Task<TrainingType> FindTrainingTypeByID(int id);
        Task AddTrainingType(TrainingType trainingType);
        Task UpdateTrainingType(TrainingType trainingType);
        Task DeleteTrainingType(int id);
    }
}
