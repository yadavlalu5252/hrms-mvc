using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly AppDbContext db;
        public DesignationService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Designation>> GetAllDesignation()
        {
            var data = await db.Designations.Include(x => x.Department).ToListAsync();
            return data;
        }

        public async Task<int> AddDesignation(Designation designation)
        {
            await db.Designations.AddAsync(designation);
            return await db.SaveChangesAsync();
        }

        public async Task<Designation?> GetDesignationById(int id)
        {
            var data = await db.Designations.Include(x => x.Department).SingleOrDefaultAsync(x => x.Id == id);
            return data;

        }

        public async Task<int> UpdateDesignation(Designation designation)
        {
           
            db.Designations.Update(designation);

            return await db.SaveChangesAsync();

        }
        public async Task<int> DeleteDesignation(int id)
        {
            var data = await db.Designations.SingleOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return 0;
            }
            db.Designations.Remove(data);

            return await db.SaveChangesAsync();
        }

    }
}
