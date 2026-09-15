using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            string? role = HttpContext.Session.GetString("Role");

            if (role != "Employee")
            {
                return RedirectToAction(
                    "Login",
                    "Auth"
                );
            }
            return View();
        }
    }
}
