using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ITaskReportService
    {
        Task<int> TotalTasks();

        Task<int> CompletedTasks();

        Task<int> OnHoldTasks();

        Task<int> OverdueTasks();

        Task<List<Tasks>> GetTaskRecords(string? priority,string? status,string? sortBy);
    }
}