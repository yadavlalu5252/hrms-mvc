using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class MasterDocAdminController : Controller
    {
        private readonly IMasterDocAdminService service;
        public MasterDocAdminController(IMasterDocAdminService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return RedirectToAction("FetchAll");
        }
        public async Task<IActionResult> FetchAll()
        {
            var data = await service.FetchAllAdminDocuments();
            return View(data);
        }

        public IActionResult AddAdminDocument()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminDocument(MasterDocAdmin adminDocument)
        {
 
                await service.AddAdminDocument(adminDocument);
                return RedirectToAction("FetchAll");

        }

        public async Task<IActionResult> EditAdminDocument(int id)
        {
            var data = await service.FindAdminDocumentByID(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAdminDocument(MasterDocAdmin adminDocument)
        {          
           await service.UpdateAdminDocument(adminDocument);
           return RedirectToAction("FetchAll");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdminDocument(int id)
        {
            var data = await service.FindAdminDocumentByID(id);
            await service.DeleteAdminDocument(id);
            return RedirectToAction("FetchAll");
        }
    }
}