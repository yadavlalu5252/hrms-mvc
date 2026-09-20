

using hrms_mvc.Repository;
using hrms_mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Spatial;

namespace hrms_mvc.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IEmployeeReportService ers;
        private readonly IAttendanceReportService ars;

        private readonly ILeaveReportService lrs;

        private readonly IProjectReportService prs;

        private readonly IDailyReportService drs;

        private readonly ITaskReportService trs;

        private readonly IPaySlipReportService pss;
        public ReportsController(IPaySlipReportService pss, IEmployeeReportService ers, IAttendanceReportService ars, ILeaveReportService lrs, IProjectReportService prs, IDailyReportService drs, ITaskReportService trs)
        {
            this.pss = pss;
            this.ers = ers;
            this.ars = ars;
            this.lrs = lrs;
            this.prs = prs;
            this.drs = drs;
            this.trs = trs;
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
            ViewBag.TotalLeaves = await lrs.TotalLeaves();

            ViewBag.ApprovedLeaves = await lrs.ApprovedLeaves();

            ViewBag.PendingLeaves = await lrs.PendingLeaves();

            ViewBag.RejectedLeaves = await lrs.RejectedLeaves();


            var records = await lrs.GetLeaveRecords(dateFilter,status,sortBy);

 
            ViewBag.DateFilter = dateFilter;
            ViewBag.Status = status; 
            ViewBag.SortBy = sortBy;

            return View(records);
        }

        public async Task<IActionResult> ProjectReport( string? priority,string? status,string? sortBy)
        {

            ViewBag.TotalProjects =await prs.TotalProjects();

            ViewBag.CompletedProjects =await prs.CompletedProjects();

            ViewBag.OverdueProjects = await prs.OverdueProjects();

            ViewBag.OnholdProjects = await prs.OnholdProjects();

            var projects =await prs.GetProjectRecords(priority,status,sortBy);

            ViewBag.Priority = priority;
            ViewBag.Status = status;
            ViewBag.SortBy = sortBy;


            return View(projects);
        }

        public async Task<IActionResult> TaskReport(string? priority,string? status,string? sortBy)
        {

            ViewBag.TotalTasks = await trs.TotalTasks();

            ViewBag.CompletedTasks = await trs.CompletedTasks();

            ViewBag.OnHoldTasks = await trs.OnHoldTasks();

            ViewBag.OverdueTasks = await trs.OverdueTasks();

            var tasks = await trs.GetTaskRecords(priority,status,sortBy);

            ViewBag.Priority = priority;
            ViewBag.Status = status;
            ViewBag.SortBy = sortBy;

            return View(tasks);
        }

        public async Task<IActionResult> DailyReport(string? status,string? sortBy)
        {

            ViewBag.TotalPresent = await drs.TotalPresent();
            ViewBag.TotalAbsent = await drs.TotalAbsent();

            ViewBag.CompletedTasks = await drs.CompletedTasks();
            ViewBag.PendingTasks = await drs.PendingTasks();

            var records = await drs.GetDailyAttendance(status, sortBy);

            ViewBag.Status = status;
            ViewBag.SortBy = sortBy;

            return View(records);
        }

        public async Task<IActionResult> PayslipReport(string? month,string? sortBy)
        {
            ViewBag.TotalPayroll = await pss.TotalPayroll();
            ViewBag.TotalDeductions = await pss.TotalDeductions();
            ViewBag.NetPay = await pss.NetPay();
            ViewBag.TotalEarnings = await pss.TotalEarnings();

            var records = await pss.GetPayslipRecords(month, sortBy);

            ViewBag.Month = month;
            ViewBag.SortBy = sortBy;

            return View(records);
        }
    }
}
