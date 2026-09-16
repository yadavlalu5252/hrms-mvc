using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext db;
        public DepartmentService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            var data = await db.Departments.ToListAsync();
            return data;
        }

        public async Task<int> AddDepartment(Department department)
        {
            await db.Departments.AddAsync(department);

            return await db.SaveChangesAsync();
        }
        
        public async Task<Department?> GetDepartmentById(int id)
        {
            var data = await db.Departments
               .SingleOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<int> UpdateDepartment(Department department)
        {
            db.Departments.Update(department);

            return await db.SaveChangesAsync();
        }
        public async Task<int> DeleteDepartment(int id)
        {
            var department = await db.Departments
                .SingleOrDefaultAsync(x => x.Id == id);

            if (department == null)
            {
                return 0;
            }

            db.Departments.Remove(department);

            return await db.SaveChangesAsync();
        
    }

          
    }
}