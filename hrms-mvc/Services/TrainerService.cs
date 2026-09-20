using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly AppDbContext db;

        public TrainerService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task AddTrainers(Trainers trainer)
        {
            await db.Trainers.AddAsync(trainer);
            await db.SaveChangesAsync();
        }

        public async Task DeleteTrainers(int id)
        {
            var temp = await db.Trainers.FindAsync(id);

            if (temp != null)
            {
                db.Trainers.Remove(temp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Trainers>> FetchAllTrainers()
        {
            var temp = await db.Trainers.ToListAsync();
            return temp;
        }

        public async Task<Trainers> FindTrainersByID(int id)
        {
            var temp = await db.Trainers.FindAsync(id);
            return temp;
        }

        public async Task UpdateTrainers(Trainers trainer)
        {
            db.Trainers.Update(trainer);
            await db.SaveChangesAsync();
        }
    }
}
