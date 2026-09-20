using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class MyDocumentController : Controller
    {
        private readonly IMyDocument service;

        public MyDocumentController(IMyDocument service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            string? email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Auth");
            }

            var documents = await service.FetchMyDocuments(email);

            return View(documents);
        }
    }
}