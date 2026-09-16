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
            var query = db.Attendances
                .Include(x => x.User)
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(x => x.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(x => x.Date <= endDate.Value);
            }

            return await query
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }


       public async Task AddAttendance(Attendance attendance)
        {
            await db.Attendances.AddAsync(attendance);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAttendance(Attendance attendance)
        {
            db.Attendances.Update(attendance);
            await db.SaveChangesAsync();

        }
        public async Task DeleteAttendance(int id)
        {
            var attendance = await db.Attendances
                .FirstOrDefaultAsync(x => x.Id == id);

            if (attendance != null)
            {
                db.Attendances.Remove(attendance);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetEmployees()
        {
            return await db.Users
                .OrderBy(x => x.FirstName)
                .ToListAsync();
        }

    }
}
