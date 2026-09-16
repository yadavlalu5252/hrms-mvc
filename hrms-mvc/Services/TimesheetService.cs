using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly AppDbContext db;

        public TimesheetService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Timesheet>> GetTimesheets()
        {
            return await db.Timesheets
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<Timesheet?> GetTimesheet(int id)
        {
            return await db.Timesheets
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetEmployees()
        {
            return await db.Users
                .ToListAsync();
        }

        public async Task AddTimesheet(Timesheet timesheet)
        {
            db.Timesheets.Add(timesheet);

            await db.SaveChangesAsync();
        }

        public async Task UpdateTimesheet(Timesheet timesheet)
        {
            var existing =
                await db.Timesheets
                    .FirstOrDefaultAsync(x => x.Id == timesheet.Id);

            if (existing != null)
            {
                existing.UserId = timesheet.UserId;
                existing.Date = timesheet.Date;
                existing.WorkHours = timesheet.WorkHours;
                existing.Status = timesheet.Status;
                existing.CreatedBy = timesheet.CreatedBy;

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteTimesheet(int id)
        {
            var timesheet =
                await db.Timesheets
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (timesheet != null)
            {
                db.Timesheets.Remove(timesheet);

                await db.SaveChangesAsync();
            }
        }

        public async Task ApproveTimesheets(int[] ids)
        {
            var timesheets =
                await db.Timesheets
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();

            foreach (var timesheet in timesheets)
            {
                timesheet.Status = "Approved";
                timesheet.ApprovedBy = "Admin";
                timesheet.ApprovedAt = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();
        }

        public async Task RejectTimesheets(int[] ids)
        {
            var timesheets =
                await db.Timesheets
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();

            foreach (var timesheet in timesheets)
            {
                timesheet.Status = "Rejected";
                timesheet.ApprovedBy = "Admin";
                timesheet.ApprovedAt = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();
        }
    }
}