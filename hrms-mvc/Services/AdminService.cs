
using hrms_mvc.Data;
using hrms_mvc.Models;
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

        public async Task<int> GetPresentEmployees(DateTime date)
        {
            var selectedDate = date.Date;
            var nextDate = selectedDate.AddDays(1);

            return await db.Attendances
                .CountAsync(x =>
                    x.Date >= selectedDate &&
                    x.Date < nextDate &&
                    x.Status == "Present");
        }

        public async Task<int> GetHalfDayEmployees(DateTime date)
        {
            var selectedDate = date.Date;
            var nextDate = selectedDate.AddDays(1);

            return await db.Attendances
                .CountAsync(x =>
                    x.Date >= selectedDate &&
                    x.Date < nextDate &&
                    x.Status == "Half Day");
        }

        public async Task<int> GetAbsentEmployees(DateTime date)
        {
            var selectedDate = date.Date;
            var nextDate = selectedDate.AddDays(1);

            return await db.Attendances
                .CountAsync(x =>
                    x.Date >= selectedDate &&
                    x.Date < nextDate &&
                    x.Status == "Absent");
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

        public async Task<List<DepartmentEmployeeViewModel>> GetEmployeesByDepartment()
        {
            var departments = await db.Departments
                .AsNoTracking()
                .Select(x => new DepartmentEmployeeViewModel
                {
                    DepartmentName = x.Name ?? "Unnamed Department",

                    EmployeeCount = db.Users
                        .Count(u => u.DepartmentId == x.Id)
                })
                .OrderByDescending(x => x.EmployeeCount)
                .ToListAsync();

            var unassignedEmployees = await db.Users
                .CountAsync(x => x.DepartmentId == null);

            if (unassignedEmployees > 0)
            {
                departments.Add(new DepartmentEmployeeViewModel
                {
                    DepartmentName = "Unassigned",
                    EmployeeCount = unassignedEmployees
                });
            }

            return departments;
        }

        public async Task<List<AttendanceDashboardViewModel>> GetClockInOutRecords(DateTime date)
        {
            var selectedDate = date.Date;
            var nextDate = selectedDate.AddDays(1);

            return await db.Attendances
                .AsNoTracking()
                .Where(x => x.Date >= selectedDate && x.Date < nextDate)
                .Select(x => new AttendanceDashboardViewModel
                {
                    UserId = x.UserId,

                    EmployeeName = (x.User.FirstName ?? "") + " " + (x.User.LastName ?? ""),

                    Date = x.Date,

                    Checkin = x.Checkin,

                    Checkout = x.Checkout,

                    Status = x.Status ?? "Not Available"
                })
                .OrderBy(x => x.EmployeeName)
                .ToListAsync();
        }
        public async Task<List<EmployeeDashboardViewModel>> GetEmployees()
        {
            return await db.Users
                .AsNoTracking()
                .Select(x => new EmployeeDashboardViewModel
                {
                    Id = x.Id,

                    EmployeeName = (x.FirstName ?? "") + " " + (x.LastName ?? ""),

                    Email = x.Email ?? "",

                    DepartmentName = x.Department != null
                        ? x.Department.Name ?? "Not Assigned"
                        : "Not Assigned",

                    Status = x.Status ?? "Not Available",

                    DateOfJoining = x.DateOfJoining
                })
                .OrderBy(x => x.EmployeeName)
                .ToListAsync();
        }
        public async Task<List<ProjectDashboardViewModel>> GetProjects()
        {
            return await db.AllProjects
                .AsNoTracking()
                .Select(x => new ProjectDashboardViewModel
                {
                    ProjectsId = x.ProjectsId,

                    ProjectName = x.ProjectName ?? "",

                    ClientName = x.ClientName ?? "",

                    ManagerName = x.ManagerName ?? "",

                    Status = x.Status ?? "Not Available",

                    TaskCount = db.Tasks
                        .Count(t => t.ProjectId == x.ProjectsId)
                })
                .OrderBy(x => x.ProjectName)
                .ToListAsync();
        }
        public async Task<List<TaskStatisticsViewModel>> GetTaskStatistics()
        {
            return await db.Tasks
                .AsNoTracking()
                .GroupBy(x => x.Status)
                .Select(x => new TaskStatisticsViewModel
                {
                    Status = x.Key ?? "Not Available",

                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();
        }
    }
}
