using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ILeaveService
    {
        Task<List<MasterLeaveType>> GetLeaveTypes();

        Task<MasterLeaveType?> GetLeaveType(int id);

        Task AddLeaveType(MasterLeaveType leaveType);

        Task UpdateLeaveType(MasterLeaveType leaveType);

        Task DeleteLeaveType(int id);

        Task<List<DepartmentLeaves>> GetDepartmentLeaves();

        Task<DepartmentLeaves?> GetDepartmentLeave(int id);

        Task AddDepartmentLeave(DepartmentLeaves departmentLeave);

        Task DeleteDepartmentLeave(int id);

        Task<List<Department>> GetDepartments();

        Task ChangeLeaveTypeStatus(int id, string status);

        Task<List<LeaveRequest>> GetMyLeaveRequests(int userId);

        Task<List<LeaveBalance>> GetMyLeaveBalances(int userId);

        Task<List<MasterLeaveType>> GetAvailableLeaveTypes(int userId);

        Task AddLeaveRequest(LeaveRequest leaveRequest);
        Task<List<DepartmentLeaves>> GetMyDepartmentLeaves(int userId);
    }
}