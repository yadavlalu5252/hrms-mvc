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
            db.DepartmentLeaves.Add(departmentLeave);

            await db.SaveChangesAsync();

            var employees = await db.Users
                .Where(x =>
                    x.DepartmentId == departmentLeave.DepartmentId &&
                    x.Status == "Active")
                .ToListAsync();

            foreach (var employee in employees)
            {
                var existingBalance =
                    await db.LeaveBalances.FirstOrDefaultAsync(x =>
                        x.UserId == employee.Id &&
                        x.DepartmentLeavesId == departmentLeave.Id);

                if (existingBalance == null)
                {
                    var balance = new LeaveBalance
                    {
                        UserId = employee.Id,
                        DepartmentLeavesId = departmentLeave.Id,
                        TotalLeaves = departmentLeave.LeavesCount,
                        UsedLeaves = 0
                    };

                    db.LeaveBalances.Add(balance);
                }
                else
                {
                    existingBalance.TotalLeaves =
                        departmentLeave.LeavesCount;
                }
            }

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

        public async Task<List<LeaveRequest>>
            GetMyLeaveRequests(int userId)
        {
            return await db.LeaveRequests
                .Include(x => x.MasterLeaveType)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<LeaveBalance>>
            GetMyLeaveBalances(int userId)
        {
            return await db.LeaveBalances
                .Include(x => x.DepartmentLeaves)
                .ThenInclude(x => x.MasterLeaveType)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<MasterLeaveType>> GetAvailableLeaveTypes(int userId)
        {
            var user = await db.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null || user.DepartmentId == null)
            {
                return new List<MasterLeaveType>();
            }

            return await db.DepartmentLeaves
                .Include(x => x.MasterLeaveType)
                .Where(x =>
                    x.DepartmentId == user.DepartmentId &&
                    x.Status == "Active" &&
                    x.MasterLeaveType.Status == "Active")
                .Select(x => x.MasterLeaveType)
                .Distinct()
                .ToListAsync();
        }

        public async Task AddLeaveRequest(
            LeaveRequest leaveRequest)
        {
            db.LeaveRequests.Add(leaveRequest);

            await db.SaveChangesAsync();
        }

        public async Task<List<DepartmentLeaves>> GetMyDepartmentLeaves(int userId)
        {
            var user = await db.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null || user.DepartmentId == null)
            {
                return new List<DepartmentLeaves>();
            }

            return await db.DepartmentLeaves
                .Include(x => x.Department)
                .Include(x => x.MasterLeaveType)
                .Where(x =>
                    x.DepartmentId == user.DepartmentId &&
                    x.Status == "Active")
                .ToListAsync();
        }
        public async Task<List<LeaveRequest>> GetManagerLeaveRequests(int managerId)
        {
            var manager = await db.Users
                .FirstOrDefaultAsync(x => x.Id == managerId);

            if (manager == null || manager.DepartmentId == null)
            {
                return new List<LeaveRequest>();
            }

            return await db.LeaveRequests
                .Include(x => x.User)
                .Include(x => x.MasterLeaveType)
                .Where(x =>
                    x.User.DepartmentId == manager.DepartmentId &&
                    x.User.RoleId == 2)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }
        public async Task ApproveLeave(
           int id,
           string approvedBy)
        {
            var leave =
                await db.LeaveRequests
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (leave != null)
            {
                leave.Status = "Approved";
                leave.ApprovedBy = approvedBy;

                await db.SaveChangesAsync();
            }
        }
        public async Task RejectLeave(
    int id,
    string approvedBy)
        {
            var leave =
                await db.LeaveRequests
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (leave != null)
            {
                leave.Status = "Rejected";
                leave.ApprovedBy = approvedBy;

                await db.SaveChangesAsync();
            }
        }
    }
}