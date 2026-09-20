using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class TrainingTypeController : Controller
    {
        private readonly ITrainingTypeService service;

        public TrainingTypeController(ITrainingTypeService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction("FetchAll");
        }

        public async Task<IActionResult> FetchAll()
        {
            var data = await service.FetchAllTrainingTypes();
            return View(data);
        }

        public IActionResult AddTrainingType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainingType(TrainingType trainingType)
        {
                await service.AddTrainingType(trainingType);
                return RedirectToAction("FetchAll");
           
        }

        public async Task<IActionResult> EditTrainingType(int id)
        {
            var data = await service.FindTrainingTypeByID(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrainingType(TrainingType trainingType)
        {
            await service.UpdateTrainingType(trainingType);
            return RedirectToAction("FetchAll");
   
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainingType(int id)
        {
            var data = await service.FindTrainingTypeByID(id);
            await service.DeleteTrainingType(id);
            return RedirectToAction("FetchAll");
        }
    }
}