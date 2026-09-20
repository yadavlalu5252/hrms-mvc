using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext db;
        public RoleService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Role>> GetAllRole()
        {
            var data = await db.Roles.ToListAsync();
            return data;
        }

        public async Task<int> AddRole(Role role)
        {
            await db.Roles.AddAsync(role);
            return await db.SaveChangesAsync();
        }

        

        public async Task<Role?> GetRoleById(int id)
        {
            var data = await db.Roles.SingleOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<int> UpdateRole(Role role)
        {
            db.Roles.Update(role);

            return await db.SaveChangesAsync();

        }
        public async Task<int> DeleteRole(int id)
        {
            var role = await db.Roles.SingleOrDefaultAsync(x => x.Id == id);
            if(role == null)
            {
                return 0;
            }
            db.Roles.Remove(role);
            return await db.SaveChangesAsync();
        }
    }
}
