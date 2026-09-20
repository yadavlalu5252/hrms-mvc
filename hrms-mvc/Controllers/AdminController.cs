using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService iaas;

        public AdminController(IAdminService iaas)
        {
            this.iaas = iaas;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            string? role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction(
                    "Login",
                    "Auth"
                );
            }

            DateTime selectedDate = date?.Date ?? DateTime.Today;

            var model = new AdminDashboardViewModel
            {
                SelectedDate = selectedDate,

                EmployeesByDepartment =
                    await iaas.GetEmployeesByDepartment(),

                ClockInOutRecords =
                    await iaas.GetClockInOutRecords(selectedDate),

                Employees =
                    await iaas.GetEmployees(),

                Projects =
                    await iaas.GetProjects(),

                TaskStatistics =
                    await iaas.GetTaskStatistics()
            };

            ViewBag.TotalEmployees =
                await iaas.GetTotalEmployees();

            ViewBag.PresentEmployees =
                await iaas.GetPresentEmployees(selectedDate);

            ViewBag.HalfDayEmployees =
                await iaas.GetHalfDayEmployees(selectedDate);

            ViewBag.AbsentEmployees =
                await iaas.GetAbsentEmployees(selectedDate);

            ViewBag.TotalProjects =
                await iaas.GetTotalProjects();

            ViewBag.TotalClients =
                await iaas.GetTotalClients();

            ViewBag.TotalTasks =
                await iaas.GetTotalTasks();

            ViewBag.TotalEarnings =
                await iaas.GetTotalEarnings();

            ViewBag.NewHires =
                await iaas.GetNewHires();

            ViewBag.ProductionHours =
                await iaas.GetProductionHours();

            ViewBag.WorkingHours =
                await iaas.GetWorkingHours();

            ViewBag.BreakHours =
                await iaas.GetBreakHours();

            ViewBag.CompletedTasks =
                await iaas.GetCompletedTasks();

            ViewBag.OnHoldTasks =
                await iaas.GetOnHoldTasks();

            ViewBag.InProgressTasks =
                await iaas.GetInProgressTasks();

            ViewBag.PendingTasks =
                await iaas.GetPendingTasks();

            return View(model);
        }
    }
}