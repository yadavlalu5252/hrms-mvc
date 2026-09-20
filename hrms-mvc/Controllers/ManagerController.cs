using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerService ims;

        public ManagerController(IManagerService ims)
        {
            this.ims = ims;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            string? role = HttpContext.Session.GetString("Role");

            if (role != "Manager")
            {
                return RedirectToAction("Login", "Auth");
            }

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            DateTime selectedDate = date?.Date ?? DateTime.Today;

            ManagerDashboardViewModel model = new ManagerDashboardViewModel
            {
                SelectedDate = selectedDate,

                Manager = await ims.GetManager(userId.Value),

                TotalWorkingHours =
                    await ims.GetTotalWorkingHours(
                        userId.Value,
                        selectedDate),

                ProductiveHours =
                    await ims.GetProductiveHours(
                        userId.Value,
                        selectedDate),

                BreakHours =
                    await ims.GetBreakHours(
                        userId.Value,
                        selectedDate),

                OvertimeHours =
                    await ims.GetOvertimeHours(
                        userId.Value,
                        selectedDate),

                Projects =
                    await ims.GetManagerProjects(
                        userId.Value),

                Tasks =
                    await ims.GetManagerTasks(
                        userId.Value)
            };

            return View(model);
        }
    }
}