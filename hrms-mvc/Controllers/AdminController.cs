using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService ias;
        public AdminController(IAdminService ias)
        {
            this.ias = ias;;
        }
        public async Task<IActionResult> Index()
        {
            string? role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction(
                    "Login",
                    "Auth"
                );
            }
            ViewBag.TotalEmployees = await ias.GetTotalEmployees();

            ViewBag.PresentEmployees = await ias.GetPresentEmployees();

            ViewBag.HalfDayEmployees = await ias.GetHalfDayEmployees();

            ViewBag.AbsentEmployees = await ias.GetAbsentEmployees();

            ViewBag.TotalProjects = await ias.GetTotalProjects();

            ViewBag.TotalClients = await ias.GetTotalClients();

            ViewBag.TotalTasks = await ias.GetTotalTasks();

            ViewBag.TotalEarnings = await ias.GetTotalEarnings();


            ViewBag.NewHires = await ias.GetNewHires();

            ViewBag.ProductionHours = await ias.GetProductionHours();

            ViewBag.WorkingHours = await ias.GetWorkingHours();

            ViewBag.BreakHours = await ias.GetBreakHours();

            ViewBag.CompletedTasks = await ias.GetCompletedTasks();

            ViewBag.OnHoldTasks = await ias.GetOnHoldTasks();

            ViewBag.InProgressTasks = await ias.GetInProgressTasks();

            ViewBag.PendingTasks = await ias.GetPendingTasks();
            return View();
        }
        
    }
}
