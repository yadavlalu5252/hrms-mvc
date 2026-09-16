using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;



namespace hrms_mvc.Services
{
    public class EmployeeReportService : IEmployeeReportService
    {
        private readonly AppDbContext db;

        public EmployeeReportService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<int> GetTotalEmployees()
        {
            return await db.Users.CountAsync();
        }

        public async Task<int> GetTotalDepartments()
        {
            return await db.Departments.CountAsync();
        }

        public async Task<int> GetActiveEmployees()
        {
            return await db.Users.CountAsync(x => x.Status == "Active");
        }

        public async Task<int> GetTotalRoles()
        {
            return await db.Roles.CountAsync();
        }

        public async Task<List<User>> GetEmployees(string? status,string? dateFilter)
        {
            var query = db.Users.Include(x => x.Department).Include(x => x.Role).Include(x => x.Designation).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            if (dateFilter == "recent")
            {
                query = query.OrderByDescending(x => x.DateOfJoining);
            }

            else if (dateFilter == "asc")
            {
                query = query.OrderBy(x => x.DateOfJoining);
            }

            else if (dateFilter == "desc")
            {
                query = query.OrderByDescending(x => x.DateOfJoining);
            }
            
            else if (dateFilter == "month")
            {
                var firstDayOfThisMonth =
                    new DateTime(
                        DateTime.Today.Year,
                        DateTime.Today.Month,
                        1);

                var firstDayOfLastMonth =
                    firstDayOfThisMonth.AddMonths(-1);

                query = query.Where(x =>
                    x.DateOfJoining >= firstDayOfLastMonth &&
                    x.DateOfJoining < firstDayOfThisMonth);
            }

            else if (dateFilter == "7days")
            {
                var today = DateTime.Today;
                var sevenDaysAgo = today.AddDays(-7);

                query = query.Where(x =>
                    x.DateOfJoining >= sevenDaysAgo &&
                    x.DateOfJoining <= today);
            }

            return await query.ToListAsync();
        }
    }
}
