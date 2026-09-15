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

        Task AddDepartmentLeave(
            DepartmentLeaves departmentLeave);

        Task DeleteDepartmentLeave(int id);

        Task<List<Department>> GetDepartments();

        Task ChangeLeaveTypeStatus(
            int id,
            string status);
    }
}