using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class LeaveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
