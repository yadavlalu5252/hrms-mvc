using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            string? role = HttpContext.Session.GetString("Role");

            if (role != "Manager")
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
