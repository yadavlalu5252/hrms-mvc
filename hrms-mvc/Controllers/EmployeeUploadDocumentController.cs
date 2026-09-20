using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class EmployeeUploadDocumentController : Controller
    {
        private readonly IEmployeeUploadDocument service;

        public EmployeeUploadDocumentController(IEmployeeUploadDocument service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string? email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Auth");
            }
            EmployeeDocumentUploadViewModel model = new EmployeeDocumentUploadViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmployeeDocumentUploadViewModel model)
        {
            string? email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Auth");
            }

                await service.SaveDocument(model, email);
                TempData["SuccessMessage"] = "Document uploaded successfully!";
                return RedirectToAction("Index");
  
        }
    }
}