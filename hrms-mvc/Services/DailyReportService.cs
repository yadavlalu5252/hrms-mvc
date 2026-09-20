using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class DailyReportService : IDailyReportService
    {
        private readonly AppDbContext db;

        public DailyReportService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<int> TotalPresent()
        {
            return await db.Attendances.CountAsync(x => x.Status == "Present");
        }

        public async Task<int> TotalAbsent()
        {
            return await db.Attendances.CountAsync(x => x.Status == "Absent");
        }

        public async Task<int> CompletedTasks()
        {
            return await db.Tasks.CountAsync(x => x.Status == "Completed");
        }

        public async Task<int> PendingTasks()
        {
            return await db.Tasks.CountAsync(x => x.Status == "Pending");
        }

        public async Task<List<Attendance>> GetDailyAttendance(string? status,string? sortBy)
        {
            var query = db.Attendances.Include(x => x.User).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            if (sortBy == "Ascending")
            {
                query = query.OrderBy(x => x.Date);
            }
            else if (sortBy == "Descending")
            {
                query = query.OrderByDescending(x => x.Date);
            }
            else if (sortBy == "Last 7 Days")
            {
                var date = DateTime.Today.AddDays(-7);

                query = query.Where(x => x.Date >= date).OrderByDescending(x => x.Date);
            }
            else if (sortBy == "Last Month")
            {
                var date = DateTime.Today.AddMonths(-1);

                query = query.Where(x => x.Date >= date).OrderByDescending(x => x.Date);
            }
            else
            {
                query = query.OrderByDescending(x => x.Date);
            }

            return await query.ToListAsync();
        }
    }
}