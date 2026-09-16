using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Net.NetworkInformation;

namespace hrms_mvc.Services
{
    public class LeaveReportService : ILeaveReportService
    {
        private readonly AppDbContext db;

        public LeaveReportService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<int> ApprovedLeaves()
        {
            return await db.LeaveRequests.CountAsync(l => l.Status == "Approved");
        }

        public async Task<List<LeaveRequest>> GetLeaveRecords(string? dateFilter, string? Status, string? SortBy)
        {
            var query = db.LeaveRequests
               .Include(x => x.User)
               .Include(x => x.MasterLeaveType)
               .AsQueryable();

            // Status Filter
            if (!string.IsNullOrEmpty(Status))
            {
                query = query.Where(x => x.Status == Status);
            }

            // Date Filter
            if (dateFilter == "Yesterday")
            {
                var yesterday = DateTime.Today.AddDays(-1);

                query = query.Where(x =>
                    x.StartDate.Date == yesterday);
            }
            else if (dateFilter == "Last 7 Days")
            {
                var date = DateTime.Today.AddDays(-7);

                query = query.Where(x =>
                    x.StartDate >= date);
            }
            else if (dateFilter == "Last 30 Days")
            {
                var date = DateTime.Today.AddDays(-30);

                query = query.Where(x =>
                    x.StartDate >= date);
            }
            else if (dateFilter == "Last Year")
            {
                var lastYear = DateTime.Today.Year - 1;

                query = query.Where(x =>
                    x.StartDate.Year == lastYear);
            }
            else if (dateFilter == "This Year")
            {
                var currentYear = DateTime.Today.Year;

                query = query.Where(x =>
                    x.StartDate.Year == currentYear);
            }

            // Sorting
            if (SortBy == "Ascending")
            {
                query = query.OrderBy(x => x.StartDate);
            }
            else if (SortBy == "Descending")
            {
                query = query.OrderByDescending(x => x.StartDate);
            }
            else if (SortBy == "Last 7 Days")
            {
                query = query
                    .OrderByDescending(x => x.StartDate);
            }
            else if (SortBy == "Last Month")
            {
                query = query
                    .OrderBy(x => x.StartDate);
            }
            else
            {
                // Default sorting
                query = query
                    .OrderByDescending(x => x.StartDate);
            }

            return await query.ToListAsync();
        }

        public async Task<int> PendingLeaves()
        {
            return await db.LeaveRequests.CountAsync(l => l.Status == "Pending");
        }

        public async Task<int> RejectedLeaves()
        {
            return await db.LeaveRequests.CountAsync(r=>r.Status == "Rejected");
        }

        public async Task<int> TotalLeaves()
        {
            return await db.LeaveRequests.CountAsync();
        }
    }
}
