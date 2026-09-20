using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly AppDbContext db;
        public PromotionService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Promotion>> GetAllPromotions()
        {
            var data = await db.Promotions.Include(x => x.User).ToListAsync();

            return data;
        }

        public async Task<Promotion?> GetPromotionById(int id)
        {
            var data = await db.Promotions
                .SingleOrDefaultAsync(x => x.PromotionId == id);

            return data;
        }

        public async Task<int> AddPromotion(Promotion promotion)
        {
            var existing = await db.Promotions
                .SingleOrDefaultAsync(x =>
                    x.UserId == promotion.UserId &&
                    x.DesignationFrom == promotion.DesignationFrom &&
                    x.DesignationTo == promotion.DesignationTo &&
                    x.Date == promotion.Date);

            if (existing != null)
            {
                return 0;
            }
            await db.Promotions.AddAsync(promotion);
            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdatePromotion(Promotion promotion)
        {
            db.Promotions.Update(promotion);

            return await db.SaveChangesAsync();
        }

        public async Task<int> DeletePromotion(int id)
        {
            var promotion = await db.Promotions
                .SingleOrDefaultAsync(x => x.PromotionId == id);

            if (promotion == null)
            {
                return 0;
            }

            db.Promotions.Remove(promotion);

            return await db.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var data = await db.Users.Where(x => x.Status == "Active").ToListAsync();
            return data;
        }

        public async Task<List<Designation>> GetAllDesignations()
        {
            var data = await db.Designations.Where(x => x.Status == "Active").ToListAsync();

            return data;
        }
    }
}