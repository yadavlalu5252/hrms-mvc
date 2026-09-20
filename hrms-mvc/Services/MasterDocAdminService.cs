using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class MasterDocAdminService : IMasterDocAdminService
    {
        private readonly AppDbContext db;

        public MasterDocAdminService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task AddAdminDocument(MasterDocAdmin adminDocument)
        {
            await db.MasterDocAdmin.AddAsync(adminDocument);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAdminDocument(int id)
        {
            var temp = await db.MasterDocAdmin.FindAsync(id);

            if (temp != null)
            {
                db.MasterDocAdmin.Remove(temp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<MasterDocAdmin>> FetchAllAdminDocuments()
        {
            var temp = await db.MasterDocAdmin.ToListAsync();

            return temp;
        }

        public async Task<MasterDocAdmin> FindAdminDocumentByID(int id)
        {
            var temp = await db.MasterDocAdmin.FindAsync(id);

            return temp;
        }

        public async Task UpdateAdminDocument(MasterDocAdmin adminDocument)
        {
            db.MasterDocAdmin.Update(adminDocument);

            await db.SaveChangesAsync();
        }
    }
}