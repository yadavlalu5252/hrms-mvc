using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext db;

        public EmployeeService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<User?> GetEmployee(int userId)
        {
            return await db.Users
                .Include(x => x.Role)
                .Include(x => x.Department)
                .Include(x => x.Designation)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<decimal> GetTotalWorkingHours(int userId, DateTime date)
        {
            return await db.Attendances
                .Where(x => x.UserId == userId && x.Date.Date == date.Date)
                .Select(x => (decimal?)x.WorkHours)
                .SumAsync() ?? 0;
        }

        public async Task<decimal> GetProductiveHours(int userId, DateTime date)
        {
            return await db.Attendances
                .Where(x => x.UserId == userId && x.Date.Date == date.Date)
                .Select(x => (decimal?)x.ProdHours)
                .SumAsync() ?? 0;
        }

        public async Task<decimal> GetBreakHours(int userId, DateTime date)
        {
            return await db.Attendances
                .Where(x => x.UserId == userId && x.Date.Date == date.Date)
                .Select(x => (decimal?)x.BreakHours)
                .SumAsync() ?? 0;
        }

        public async Task<decimal> GetOvertimeHours(int userId, DateTime date)
        {
            return await db.Attendances
                .Where(x => x.UserId == userId && x.Date.Date == date.Date)
                .Select(x => (decimal?)x.OTHours)
                .SumAsync() ?? 0;
        }

        public async Task<List<EmployeeProjectViewModel>> GetEmployeeProjects(int userId)
        {
            return new List<EmployeeProjectViewModel>();
        }

        public async Task<List<EmployeeTaskViewModel>> GetEmployeeTasks(int userId)
        {
            return new List<EmployeeTaskViewModel>();
        }
    }
}