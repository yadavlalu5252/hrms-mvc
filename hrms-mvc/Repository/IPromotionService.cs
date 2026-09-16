using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IPromotionService
    {
        Task<List<Promotion>> GetAllPromotions();
        Task<Promotion?> GetPromotionById(int id);
        Task<int> AddPromotion(Promotion promotion);
        Task<int> UpdatePromotion(Promotion promotion);
        Task<int> DeletePromotion(int id);
        Task<List<User>> GetAllUsers();
        Task<List<Designation>> GetAllDesignations();
    }
}
