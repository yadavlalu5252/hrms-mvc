using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AdminUploadFileListController : Controller
    {
        private readonly IUploadedDocument service;

        public AdminUploadFileListController(IUploadedDocument service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var files = await service.FetchAllFiles();
            return View(files);
        }

        public async Task<IActionResult> ViewFile(int id)
        {
            var file = await service.FindById(id);

            if (file == null)
            {
                return NotFound();
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.DocFile);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(filePath, "application/pdf");
            
        }

        public async Task<IActionResult> Download(int id)
        {
            var file = await service.FindById(id);

            if (file == null)
            {
                return NotFound();
            }

            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                file.DocFile
            );

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(filePath, "application/octet-stream", file.DocFile);
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteFile(id);
            return RedirectToAction("Index");
        }
    }
}