using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
