namespace hrms_mvc.Repository
{
    public interface IAdminService
    {
        Task<int> GetTotalEmployees();

        Task<int> GetPresentEmployees();

        Task<int> GetHalfDayEmployees();

        Task<int> GetAbsentEmployees();

        Task<int> GetTotalProjects();

        Task<int> GetTotalClients();

        Task<int> GetTotalTasks();

        Task<decimal> GetTotalEarnings();


        Task<int> GetNewHires();

        Task<decimal> GetProductionHours();

        Task<decimal> GetWorkingHours();

        Task<decimal> GetBreakHours();

        Task<int> GetCompletedTasks();

        Task<int> GetOnHoldTasks();

        Task<int> GetInProgressTasks();

        Task<int> GetPendingTasks();

    }
}
