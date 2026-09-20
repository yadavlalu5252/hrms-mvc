using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AdminFilesUploadController : Controller
    {
        private readonly IAdminFileUpload service;

        public AdminFilesUploadController(IAdminFileUpload service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            AdminFileUploadViewModel model = new AdminFileUploadViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdminFileUploadViewModel model)
        {
      
                await service.SaveFile(model);
                TempData["SuccessMessage"] = "Document uploaded successfully!";

            return RedirectToAction("Index");
    
        }
    }
}