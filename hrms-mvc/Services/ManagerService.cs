using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class ManagerService : IManagerService
    {
        private readonly AppDbContext db;

        public ManagerService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<User?> GetManager(int userId)
        {
            return await db.Users
                .Include(x => x.Role)
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<decimal> GetTotalWorkingHours(
     int userId,
     DateTime date)
        {
            return await db.Attendances
                .Where(x => x.UserId == userId && x.Date.Date == date.Date)
                .Select(x => (decimal?)x.WorkHours)
                .SumAsync() ?? 0;
        }

        public async Task<decimal> GetProductiveHours(
    int userId,
    DateTime date)
        {
            return await db.Attendances
                .Where(x => x.UserId == userId && x.Date.Date == date.Date)
                .Select(x => (decimal?)x.ProdHours)
                .SumAsync() ?? 0;
        }

        public async Task<decimal> GetBreakHours(
    int userId,
    DateTime date)
        {
            return await db.Attendances
                .Where(x => x.UserId == userId && x.Date.Date == date.Date)
                .Select(x => (decimal?)x.BreakHours)
                .SumAsync() ?? 0;
        }

        public async Task<decimal> GetOvertimeHours(
    int userId,
    DateTime date)
        {
            return await db.Attendances
                .Where(x => x.UserId == userId && x.Date.Date == date.Date)
                .Select(x => (decimal?)x.OTHours)
                .SumAsync() ?? 0;
        }

        public async Task<List<ManagerProjectViewModel>> GetManagerProjects(
            int userId)
        {
            var manager = await db.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (manager == null)
            {
                return new List<ManagerProjectViewModel>();
            }

            string managerName =
                (manager.FirstName + " " + manager.LastName).Trim();

            return await db.AllProjects
                .Where(x => x.ManagerName == managerName)
                .Select(x => new ManagerProjectViewModel
                {
                    ProjectId = x.ProjectsId,
                    ProjectName = x.ProjectName,
                    ClientName = x.ClientName,
                    Status = x.Status,
                    TotalTasks = db.Tasks.Count(
                        task => task.ProjectId == x.ProjectsId)
                })
                .ToListAsync();
        }

        public async Task<List<ManagerTaskViewModel>> GetManagerTasks(
            int userId)
        {
            var manager = await db.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (manager == null)
            {
                return new List<ManagerTaskViewModel>();
            }

            string managerName =
                (manager.FirstName + " " + manager.LastName).Trim();

            var managerProjectIds = await db.AllProjects
                .Where(x => x.ManagerName == managerName)
                .Select(x => x.ProjectsId)
                .ToListAsync();

            return await db.Tasks
                .AsNoTracking()
                .Where(x => managerProjectIds.Contains(x.ProjectId))
                .Select(x => new ManagerTaskViewModel
                {
                    TaskId = x.TasksId,
                    Title = x.Title,
                    ProjectName = x.Project.ProjectName,
                    Status = x.Status,
                    Priority = x.Priority,
                    Deadline = x.Deadline
                })
                .ToListAsync();
        }
    }
}