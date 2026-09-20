using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class PaySlipReportService : IPaySlipReportService
    {
        private readonly AppDbContext db;

        public PaySlipReportService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<decimal> TotalPayroll()
        {
            return await db.EmployeeSalaries.SumAsync(x => x.TotalSalary);
        }

        public async Task<decimal> TotalDeductions()
        {
            return await db.EmployeeDeductions.SumAsync(x => x.DeductionAmount);
        }

        public async Task<decimal> NetPay()
        {
            return await db.EmployeeSalaries.SumAsync(x => x.NetSalary);
        }

        public async Task<decimal> TotalEarnings()
        {
            return await db.EmployeeEarnings.SumAsync(x => x.EarningAmount);
        }

        public async Task<List<Payslips>> GetPayslipRecords(string? month,string? sortBy)
        {
            var query = db.Payslips.Include(x => x.User).AsQueryable();

            if (!string.IsNullOrEmpty(month))
            {
                query = query.Where(x => x.Month == month);
            }

            if (sortBy == "Ascending")
            {
                query = query.OrderBy(x => x.Id);
            }
            else if (sortBy == "Descending")
            {
                query = query.OrderByDescending(x => x.Id);
            }
            else
            {
                query = query.OrderByDescending(x => x.GeneratedOn);
            }

            return await query.ToListAsync();
        }
    }
}