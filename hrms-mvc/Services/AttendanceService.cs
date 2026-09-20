using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AppDbContext db;

        public AttendanceService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Attendance>> GetAttendanceByUserId(int userId)
        {
            return await db.Attendances
           .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<List<Attendance>> GetAttendanceList()
        {
            return await db.Attendances
                .Include(a => a.User)
                .OrderByDescending(a => a.Id)
                .ToListAsync();
        }

        public async Task<Attendance?> GetAttendanceById(int id)
        {
            return await db.Attendances
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Attendance>> GetAttendanceByDate(
            DateTime? startDate,
            DateTime? endDate)
        {
            IQueryable<Attendance> query =
                db.Attendances
                    .Include(a => a.User);

            if (startDate.HasValue)
            {
                query = query.Where(a =>
                    a.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(a =>
                    a.Date <= endDate.Value);
            }

            return await query
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task UpdateAttendance(Attendance attendance)
        {
            db.Attendances.Update(attendance);

            await db.SaveChangesAsync();
        }

        public async Task DeleteAttendance(int id)
        {
            var attendance =
                await db.Attendances
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (attendance != null)
            {
                db.Attendances.Remove(attendance);

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetEmployees()
        {
            return await db.Users
                .OrderBy(a => a.FirstName)
                .ToListAsync();
        }

        public async Task<Attendance?> GetTodayAttendance(int userId)
        {
            var today = DateTime.Today;

            return await db.Attendances
                .FirstOrDefaultAsync(a =>
                    a.UserId == userId &&
                    a.Date.Date == today);
        }

        public async Task CheckIn(int userId)
        {
            var attendance =
                await GetTodayAttendance(userId);

            if (attendance == null)
            {
                attendance = new Attendance
                {
                    UserId = userId,
                    Date = DateTime.Today,
                    Checkin = DateTime.Now,
                    Status = "Present",
                    WorkHours = 0,
                    ProdHours = 0,
                    BreakHours = 0,
                    OTHours = 0,
                    Late = 0
                };

                await db.Attendances.AddAsync(attendance);
            }

            await db.SaveChangesAsync();
        }

        public async Task LunchOut(int userId)
        {
            var attendance =
                await GetTodayAttendance(userId);

            if (attendance == null)
            {
                return;
            }

            if (attendance.Checkin == null)
            {
                return;
            }

            if (attendance.Lunchout != null)
            {
                return;
            }

            if (attendance.Checkout != null)
            {
                return;
            }

            attendance.Lunchout = DateTime.Now;

            await db.SaveChangesAsync();
        }

        public async Task LunchIn(int userId)
        {
            var attendance =
                await GetTodayAttendance(userId);

            if (attendance == null)
            {
                return;
            }

            if (attendance.Lunchout == null)
            {
                return;
            }

            if (attendance.Lunchin != null)
            {
                return;
            }

            if (attendance.Checkout != null)
            {
                return;
            }

            attendance.Lunchin = DateTime.Now;

            await db.SaveChangesAsync();
        }

        public async Task CheckOut(int userId)
        {
            var attendance =
                await GetTodayAttendance(userId);

            if (attendance == null)
            {
                return;
            }

            if (attendance.Checkin == null)
            {
                return;
            }

            if (attendance.Lunchout == null)
            {
                return;
            }

            if (attendance.Lunchin == null)
            {
                return;
            }

            if (attendance.Checkout != null)
            {
                return;
            }

            attendance.Checkout = DateTime.Now;

            CalculateHours(attendance);

            attendance.Status = "Present";

            await db.SaveChangesAsync();
        }

        private void CalculateHours(Attendance attendance)
        {
            if (!attendance.Checkin.HasValue ||
                !attendance.Checkout.HasValue)
            {
                return;
            }

            var totalWorkTime =
                attendance.Checkout.Value -
                attendance.Checkin.Value;

            attendance.WorkHours =
                Convert.ToDecimal(
                    totalWorkTime.TotalHours);

            if (attendance.Lunchout.HasValue &&
                attendance.Lunchin.HasValue)
            {
                var lunchDuration =
                    attendance.Lunchin.Value -
                    attendance.Lunchout.Value;

                attendance.BreakHours =
                    Convert.ToDecimal(
                        lunchDuration.TotalHours);
            }
            else
            {
                attendance.BreakHours = 0;
            }

            attendance.ProdHours =
                attendance.WorkHours -
                attendance.BreakHours;

            if (attendance.ProdHours > 8)
            {
                attendance.OTHours =
                    attendance.ProdHours - 8;
            }
            else
            {
                attendance.OTHours = 0;
            }
        }
    }
}