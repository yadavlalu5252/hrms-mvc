using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class ProjectReportService : IProjectReportService
    {
        private readonly AppDbContext db;

        public ProjectReportService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<int> TotalProjects()
        {
            return await db.AllProjects.CountAsync();
        }

        public async Task<int> CompletedProjects()
        {
            return await db.AllProjects.CountAsync(p => p.Status == "Completed");
        }

        public async Task<int> OverdueProjects()
        {
            return await db.AllProjects.CountAsync( p => p.EndDate < DateTime.Today &&p.Status != "Completed");
        }

        public async Task<int> OnholdProjects()
        {
            return await db.AllProjects.CountAsync(p => p.Status == "Onhold");
        }

        public async Task<List<Projects>> GetProjectRecords(string? priority, string? status,string? sortBy)
        {
            var query = db.AllProjects.AsQueryable();

            if (!string.IsNullOrEmpty(priority))
            {
                query = query.Where(p =>p.Priority == priority);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p =>p.Status == status);
            }

            if (sortBy == "Ascending")
            {
                query = query.OrderBy(p => p.ProjectName);
            }
            else if (sortBy == "Descending")
            {
                query = query.OrderByDescending(p => p.ProjectName);
            }
            else if (sortBy == "Deadline")
            {
                query = query.OrderBy(p => p.EndDate);
            }
            else if (sortBy == "DeadlineDescending")
            {
                query = query.OrderByDescending(p => p.EndDate);
            }
            else if (sortBy == "Priority")
            {
                query = query.OrderBy(p => p.Priority);
            }
            else
            {
                query = query.OrderBy(p => p.ProjectsId);
            }

            return await query.ToListAsync();
        }
    }
}