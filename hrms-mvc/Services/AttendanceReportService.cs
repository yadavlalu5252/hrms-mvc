using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class AttendanceReportService : IAttendanceReportService
    {
        private readonly AppDbContext db; 
        public AttendanceReportService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Attendance>> GetAttendanceRecords( string? dateFilter,string? status,string? sortBy)
        {
            var query = db.Attendances.Include(x => x.User).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            if (dateFilter == "Yesterday")
            {
                var yesterday = DateTime.Today.AddDays(-1);
                query = query.Where(x => x.Date.Date == yesterday);
            }
            else if (dateFilter == "Last 7 Days")
            {
                query = query.Where(x => x.Date >= DateTime.Today.AddDays(-7));
            }
            else if (dateFilter == "Last 30 Days")
            {
                query = query.Where(x => x.Date >= DateTime.Today.AddDays(-30));
            }
            else if (dateFilter == "Last Year")
            {
                query = query.Where(x => x.Date.Year == DateTime.Today.Year - 1);
            }
            else if (dateFilter == "This Year")
            {
                query = query.Where(x => x.Date.Year == DateTime.Today.Year);
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
                query = query.OrderByDescending(x => x.Date);
            }
            else if (sortBy == "Last Month")
            {
                query = query.OrderBy(x => x.Date);
            }

            return await query.ToListAsync();
        }

        public async Task<int> TotalHalfDays()
        {
            return 0;
           // return await db.Events.CountAsync(x => x.EventType == "Half Day");
        }

        public async Task<int> TotalHolidays()
        {
            return 0;
           // return await db.Events.CountAsync(x => x.EventType == "Holiday");
        }

        public async Task<int> TotalLeaveTaken()
        {
            return await db.LeaveRequests.Where(x=>x.Status=="Approved").SumAsync(x=>x.NumberOfDays);
        }

        public async Task<int> TotalWorkingDays()
        {
            return await db.Attendances.CountAsync(x=>x.Status != "Absent");
        }
    }
}
