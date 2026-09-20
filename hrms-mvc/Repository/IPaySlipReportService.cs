using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IPaySlipReportService
    {
        Task<decimal> TotalPayroll();

        Task<decimal> TotalDeductions();

        Task<decimal> NetPay();

        Task<decimal> TotalEarnings();

        Task<List<Payslips>> GetPayslipRecords(string? month,string? sortBy);
    }
}