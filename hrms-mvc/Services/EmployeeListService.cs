using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class EmployeeListService : IEmployeeListService
    {
        private readonly AppDbContext db;
        public EmployeeListService(AppDbContext db)
        {
            this.db = db;
        }


        public async Task<List<User>> GetAllEmployees()
        {
            var data = await db.Users
                  .Include(x => x.Role)
                  .Include(x => x.Department)
                  .Include(x => x.Designation)
                  .ToListAsync();
            return data;

        }


        public async Task<int> AddEmployee(User user)
        {
             await db.Users.AddAsync(user);
             return await db.SaveChangesAsync();
        }


        public async Task<User?> FindEmployeeById(int id)
        {
            var data = await db.Users
                .Include(x =>x.Role)
                .Include(x=>x.Designation)
                .Include(x=>x.Department)
                .SingleOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<int> UpdateEmployee(User user)
        {
             db.Users.Update(user);
             return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteEmployee(int id)
        {
            var data = await db.Users.SingleOrDefaultAsync(x=>x.Id==id);
            if(data == null)
            {
                return 0;
            }
            db.Users.Remove(data);
            return await db.SaveChangesAsync();
        }


    }
}
