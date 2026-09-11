using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
