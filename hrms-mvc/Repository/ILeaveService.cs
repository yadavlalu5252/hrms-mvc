using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ILeaveService
    {
        List<MasterLeaveType> GetLeaveTypes();

        MasterLeaveType? GetLeaveType(int id);

        void AddLeaveType(MasterLeaveType leaveType);

        void DeleteLeaveType(int id);

        List<DepartmentLeaves> GetDepartmentLeaves();

        DepartmentLeaves? GetDepartmentLeave(int id);

        void AddDepartmentLeave(DepartmentLeaves departmentLeave);

        void DeleteDepartmentLeave(int id);

        List<Department> GetDepartments();

        void ChangeLeaveTypeStatus(int id, string status);
    }
}