using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class TrainerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
