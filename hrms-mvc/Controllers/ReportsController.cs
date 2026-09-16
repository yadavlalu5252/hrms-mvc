using hrms_mvc.Repository;
using hrms_mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IEmployeeReportService ers;
        private readonly IAttendanceReportService ars;

        private readonly ILeaveReportService lrs;

        public ReportsController(IEmployeeReportService ers, IAttendanceReportService ars, ILeaveReportService lrs)
        {
            this.ers = ers;
            this.ars = ars;
            this.lrs = lrs;
        }

        public async Task<IActionResult> Index(string? status,string? dateFilter)
        {
            ViewBag.TotalEmployees =await ers.GetTotalEmployees();

            ViewBag.TotalDepartments =await ers.GetTotalDepartments();

            ViewBag.ActiveEmployees =await ers.GetActiveEmployees();

            ViewBag.TotalRoles =await ers.GetTotalRoles();

            var employees =await ers.GetEmployees(status, dateFilter);

            ViewBag.SelectedStatus = status;
            ViewBag.SelectedDateFilter = dateFilter;

            return View(employees);
        }

        public async Task<IActionResult> Attendance(string? dateFilter,string? status,string? sortBy)
        {
            ViewBag.TotalWorkingDays = await ars.TotalWorkingDays();

            ViewBag.TotalLeaveTaken = await ars.TotalLeaveTaken();

            ViewBag.TotalHolidays = await ars.TotalHolidays();

            ViewBag.TotalHalfDays = await ars.TotalHalfDays();

            var attendanceRecords = await ars.GetAttendanceRecords(dateFilter, status, sortBy);

            return View(attendanceRecords);
        }

        public async Task<IActionResult> LeaveReport(string? dateFilter,string? status,string? sortBy)
        {
            // Summary cards
            ViewBag.TotalLeaves = await lrs.TotalLeaves();

            ViewBag.ApprovedLeaves = await lrs.ApprovedLeaves();

            ViewBag.PendingLeaves = await lrs.PendingLeaves();

            ViewBag.RejectedLeaves = await lrs.RejectedLeaves();

            // Leave records
            var records =await lrs.GetLeaveRecords(dateFilter,status,sortBy);

            // Keep selected filter values
            ViewBag.DateFilter = dateFilter;
            ViewBag.Status = status;
            ViewBag.SortBy = sortBy;

            return View(records);
        }
    }
}