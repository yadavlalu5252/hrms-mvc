using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            string? role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
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
