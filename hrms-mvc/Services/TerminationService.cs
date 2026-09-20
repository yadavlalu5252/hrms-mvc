using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class TerminationService : ITerminationService
    {
        private readonly AppDbContext db;
        public TerminationService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Termination>> GetAllTerminations()
        {
            var data = await db.Terminations.Include(x => x.User).ToListAsync();
            return data;
        }

        public async Task<Termination?> GetTerminationById(int id)
        {
            var data = await db.Terminations.SingleOrDefaultAsync(x => x.TerminationId == id);
            return data;
        }

        public async Task<int> AddTermination(Termination termination)
        {
            var existing = await db.Terminations
                   .SingleOrDefaultAsync(x =>
                    x.UserId == termination.UserId &&
                    x.TerminationType == termination.TerminationType &&
                    x.NoticeDate == termination.NoticeDate &&
                    x.ResignDate == termination.ResignDate);

            if (existing != null)
            {
                return 0;
            }
            await db.Terminations.AddAsync(termination);
            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdateTermination(Termination termination)
        {
            db.Terminations.Update(termination);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteTermination(int id)
        {
            var termination = await db.Terminations.SingleOrDefaultAsync(x => x.TerminationId == id);

            if (termination == null)
            {
                return 0;
            }

            db.Terminations.Remove(termination);
            return await db.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var data = await db.Users.ToListAsync();
            return data;
        }
    }
}