using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class CompanyLettersController : Controller
    {
        private readonly ICompanyLetters service;

        public CompanyLettersController(ICompanyLetters service)
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

            var letters = await service.FetchCompanyLetters(email);

            return View(letters);
        }
    }
}