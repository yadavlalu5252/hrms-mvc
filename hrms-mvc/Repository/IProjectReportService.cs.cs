using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IProjectReportService
    {
        Task<int> TotalProjects();
        Task<int> CompletedProjects();
        Task<int> OverdueProjects();
        Task<int> OnholdProjects();

        Task<List<Projects>> GetProjectRecords(string? priority,string? status,string? sortBy);
    }
}