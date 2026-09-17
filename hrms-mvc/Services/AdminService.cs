
using hrms_mvc.Data;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;
namespace hrms_mvc.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext db;
        public AdminService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<int> GetTotalEmployees()
        {
            return await db.Users.CountAsync();
        }

        public async Task<int> GetPresentEmployees()
        {
            return await db.Attendances
                .CountAsync(x => x.Status == "Present");
        }

        public async Task<int> GetHalfDayEmployees()
        {
            return await db.Attendances
                .CountAsync(x => x.Status == "Half Day");
        }

        public async Task<int> GetAbsentEmployees()
        {
            return await db.Attendances
                .CountAsync(x => x.Status == "Absent");
        }

        public async Task<int> GetTotalProjects()
        {
            return await db.AllProjects.CountAsync();
        }

        public async Task<int> GetTotalClients()
        {
            return await db.Users
                .Where(x => x.Role != null && x.Role.RoleName == "Client")
                .CountAsync();
        }

        public async Task<int> GetTotalTasks()
        {
            return await db.Tasks.CountAsync();
        }

        public async Task<decimal> GetTotalEarnings()
        {
            return await db.Earning
                .Select(x => (decimal?)x.EarningsPercentage)
                .SumAsync() ?? 0;
        }


        public async Task<int> GetNewHires()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            return await db.Users
                .CountAsync(x =>
                    x.DateOfJoining.Month == currentMonth &&
                    x.DateOfJoining.Year == currentYear);
        }

        public async Task<decimal> GetProductionHours()
        {
            return await db.Attendances
                .Select(x => (decimal?)x.ProdHours)
                .SumAsync() ?? 0;
        }

        public async Task<decimal> GetWorkingHours()
        {
            return await db.Attendances
                .Select(x => (decimal?)x.WorkHours)
                .SumAsync() ?? 0;
        }

        public async Task<decimal> GetBreakHours()
        {
            return await db.Attendances
                .Select(x => (decimal?)x.BreakHours)
                .SumAsync() ?? 0;
        }

        public async Task<int> GetCompletedTasks()
        {
            return await db.Tasks
                .CountAsync(x => x.Status == "Completed");
        }

        public async Task<int> GetOnHoldTasks()
        {
            return await db.Tasks
                .CountAsync(x => x.Status == "On Hold");
        }

        public async Task<int> GetInProgressTasks()
        {
            return await db.Tasks
                .CountAsync(x => x.Status == "In Progress");
        }

        public async Task<int> GetPendingTasks()
        {
            return await db.Tasks
                .CountAsync(x => x.Status == "Pending");
        }
    }
}
