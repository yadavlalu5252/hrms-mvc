using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class MasterDocEmpService : IMasterDocEmpService
    {
        private readonly AppDbContext db;

        public MasterDocEmpService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task AddEmpDoc(MasterDocEmp empDoc)
        {
            await db.MasterDocEmp.AddAsync(empDoc);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEmpDoc(int id)
        {
            var temp = await db.MasterDocEmp.FindAsync(id);

            if (temp != null)
            {
                db.MasterDocEmp.Remove(temp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<MasterDocEmp>> FetchAllEmpDoc()
        {
            var temp = await db.MasterDocEmp.ToListAsync();
            return temp;
        }

        public async Task<MasterDocEmp> FindEmpDocByID(int id)
        {
            var temp = await db.MasterDocEmp.FindAsync(id);
            return temp;
        }

        public async Task UpdateEmpDoc(MasterDocEmp empDoc)
        {
            db.MasterDocEmp.Update(empDoc);
            await db.SaveChangesAsync();
        }
    }
}