using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class ResignationService : IResignationService
    {
        private readonly AppDbContext db;
        public ResignationService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Resignation>> GetAllResignations()
        {
            var data = await db.Resignations.Include(x => x.User).Include(x => x.Department).ToListAsync();
            return data;
        }

        public async Task<Resignation?> GetResignationById(int id)
        {
            var data = await db.Resignations.SingleOrDefaultAsync(x => x.ResignationId == id);
            return data;
        }

        public async Task<int> AddResignation(Resignation resignation)
        {
            var existing = await db.Resignations
                   .SingleOrDefaultAsync(x =>
                    x.UserId == resignation.UserId &&
                    x.NoticeDate == resignation.NoticeDate &&
                    x.ResignDate == resignation.ResignDate);

            if (existing != null)
            {
                return 0;
            }
            await db.Resignations.AddAsync(resignation);
            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdateResignation(Resignation resignation)
        {
            db.Resignations.Update(resignation);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteResignation(int id)
        {
            var resignation = await db.Resignations.SingleOrDefaultAsync(x => x.ResignationId == id);

            if (resignation == null)
            {
                return 0;
            }

            db.Resignations.Remove(resignation);
            return await db.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var data = await db.Users.ToListAsync();
            return data;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            var data = await db.Departments.ToListAsync();
            return data;
        }
    }
}