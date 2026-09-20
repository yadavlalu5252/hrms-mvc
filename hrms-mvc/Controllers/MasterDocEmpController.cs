using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class MasterDocEmpController : Controller
    {
        private readonly IMasterDocEmpService service;

        public MasterDocEmpController(IMasterDocEmpService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction("FetchAllEmpDoc");
        }

        public async Task<IActionResult> FetchAllEmpDoc()
        {
            var data = await service.FetchAllEmpDoc();
            return View(data);
        }

        public IActionResult AddEmpDoc()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmpDoc(MasterDocEmp empDoc)
        {
            
                await service.AddEmpDoc(empDoc);
                return RedirectToAction("FetchAllEmpDoc");
           
        }

        public async Task<IActionResult> EditEmpDoc(int id)
        {
            var data = await service.FindEmpDocByID(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmpDoc(MasterDocEmp empDoc)
        {
        
            await service.UpdateEmpDoc(empDoc);
            return RedirectToAction("FetchAllEmpDoc");
   
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmpDoc(int id)
        {
            var data = await service.FindEmpDocByID(id);
            await service.DeleteEmpDoc(id);
            return RedirectToAction("FetchAllEmpDoc");
        }
    }
}