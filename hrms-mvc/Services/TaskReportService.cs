using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class TaskReportService : ITaskReportService
    {
        private readonly AppDbContext db;

        public TaskReportService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<int> TotalTasks()
        {
            return await db.Tasks.CountAsync();
        }

        public async Task<int> CompletedTasks()
        {
            return await db.Tasks.CountAsync(x => x.Status == "Completed");
        }


        public async Task<int> OnHoldTasks()
        {
            return await db.Tasks.CountAsync(x => x.Status == "On Hold");
        }

        public async Task<int> OverdueTasks()
        {
            return await db.Tasks.CountAsync(x =>x.Deadline < DateTime.Today &&x.Status != "Completed");
        }

        public async Task<List<Tasks>> GetTaskRecords(string? priority,string? status,string? sortBy)
        {
            var query = db.Tasks.Include(x => x.Project).AsQueryable();

            if (!string.IsNullOrEmpty(priority))
            {
                query = query.Where(x => x.Priority == priority);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            if (sortBy == "Ascending")
            {
                query = query.OrderBy(x => x.Title);
            }
            else if (sortBy == "Descending")
            {
                query = query.OrderByDescending(x => x.Title);
            }
            else if (sortBy == "Deadline")
            {
                query = query.OrderBy(x => x.Deadline);
            }
            else if (sortBy == "Recent")
            {
                query = query.OrderByDescending(x => x.Deadline);
            }
            else
            {
                query = query.OrderBy(x => x.Deadline);
            }

            return await query.ToListAsync();
        }
    }
}