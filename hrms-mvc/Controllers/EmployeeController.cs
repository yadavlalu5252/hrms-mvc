using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService ies;

        public EmployeeController(IEmployeeService ies)
        {
            this.ies = ies;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            string? role = HttpContext.Session.GetString("Role");

            if (role != "Employee")
            {
                return RedirectToAction("Login", "Auth");
            }

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            DateTime selectedDate = date?.Date ?? DateTime.Today;

            EmployeeDashboardViewModel model = new EmployeeDashboardViewModel
            {
                SelectedDate = selectedDate,

                Employee = await ies.GetEmployee(userId.Value),

                TotalWorkingHours =
                    await ies.GetTotalWorkingHours(userId.Value, selectedDate),

                ProductiveHours =
                    await ies.GetProductiveHours(userId.Value, selectedDate),

                BreakHours =
                    await ies.GetBreakHours(userId.Value, selectedDate),

                OvertimeHours =
                    await ies.GetOvertimeHours(userId.Value, selectedDate),

                Projects =
                    await ies.GetEmployeeProjects(userId.Value),

                Tasks =
                    await ies.GetEmployeeTasks(userId.Value)
            };

            return View(model);
        }
    }
}