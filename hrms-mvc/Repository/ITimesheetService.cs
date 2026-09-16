
using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ITimesheetService
    {
        Task<List<Timesheet>> GetTimesheets();

        Task<Timesheet?> GetTimesheet(int id);

        Task<List<User>> GetEmployees();

        Task AddTimesheet(Timesheet timesheet);

        Task UpdateTimesheet(Timesheet timesheet);

        Task DeleteTimesheet(int id);

        Task ApproveTimesheets(int[] ids);

        Task RejectTimesheets(int[] ids);
    }
}