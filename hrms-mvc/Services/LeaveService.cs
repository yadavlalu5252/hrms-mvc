using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly AppDbContext db;

        public LeaveService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<MasterLeaveType>> GetLeaveTypes()
        {
            return await db.MasterLeaveTypes
                .ToListAsync();
        }

        public async Task<MasterLeaveType?> GetLeaveType(int id)
        {
            return await db.MasterLeaveTypes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddLeaveType(
            MasterLeaveType leaveType)
        {
            db.MasterLeaveTypes.Add(leaveType);

            await db.SaveChangesAsync();
        }

        public async Task UpdateLeaveType(
            MasterLeaveType leaveType)
        {
            var existingLeave =
                await db.MasterLeaveTypes
                    .FirstOrDefaultAsync(
                        x => x.Id == leaveType.Id);

            if (existingLeave != null)
            {
                existingLeave.Leave =
                    leaveType.Leave;

                existingLeave.Status =
                    leaveType.Status;

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteLeaveType(int id)
        {
            var leaveType =
                await db.MasterLeaveTypes
                    .FirstOrDefaultAsync(
                        x => x.Id == id);

            if (leaveType != null)
            {
                db.MasterLeaveTypes.Remove(
                    leaveType);

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<DepartmentLeaves>>
            GetDepartmentLeaves()
        {
            return await db.DepartmentLeaves
                .Include(x => x.Department)
                .Include(x => x.MasterLeaveType)
                .ToListAsync();
        }

        public async Task<DepartmentLeaves?>
            GetDepartmentLeave(int id)
        {
            return await db.DepartmentLeaves
                .Include(x => x.Department)
                .Include(x => x.MasterLeaveType)
                .FirstOrDefaultAsync(
                    x => x.Id == id);
        }

        public async Task AddDepartmentLeave(
            DepartmentLeaves departmentLeave)
        {
            db.DepartmentLeaves.Add(
                departmentLeave);

            await db.SaveChangesAsync();
        }

        public async Task DeleteDepartmentLeave(int id)
        {
            var departmentLeave =
                await db.DepartmentLeaves
                    .FirstOrDefaultAsync(
                        x => x.Id == id);

            if (departmentLeave != null)
            {
                db.DepartmentLeaves.Remove(
                    departmentLeave);

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Department>>
            GetDepartments()
        {
            return await db.Departments
                .ToListAsync();
        }

        public async Task ChangeLeaveTypeStatus(
            int id,
            string status)
        {
            var leaveType =
                await db.MasterLeaveTypes
                    .FirstOrDefaultAsync(
                        x => x.Id == id);

            if (leaveType != null)
            {
                leaveType.Status = status;

                await db.SaveChangesAsync();
            }
        }
    }
}