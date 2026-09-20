using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class TrainingTypeService : ITrainingTypeService
    {
        private readonly AppDbContext db;

        public TrainingTypeService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task AddTrainingType(TrainingType trainingType)
        {
            await db.TrainingType.AddAsync(trainingType);
            await db.SaveChangesAsync();
        }

        public async Task DeleteTrainingType(int id)
        {
            var temp = await db.TrainingType.FindAsync(id);

            if (temp != null)
            {
                db.TrainingType.Remove(temp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<TrainingType>> FetchAllTrainingTypes()
        {
            var temp = await db.TrainingType.ToListAsync();
            return temp;
        }

        public async Task<TrainingType> FindTrainingTypeByID(int id)
        {
            var temp = await db.TrainingType.FindAsync(id);
            return temp;
        }

        public async Task UpdateTrainingType(TrainingType trainingType)
        {
            db.TrainingType.Update(trainingType);
            await db.SaveChangesAsync();
        }
    }
}