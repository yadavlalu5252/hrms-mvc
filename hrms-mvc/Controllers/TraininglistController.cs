using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hrms_mvc.Controllers
{
    public class TrainingListController : Controller
    {
        private readonly ITrainingListService service;
        private readonly ITrainerService trainerService;
        private readonly ITrainingTypeService trainingTypeService;

        public TrainingListController(ITrainingListService service,ITrainerService trainerService,ITrainingTypeService trainingTypeService)
        {
            this.service = service;
            this.trainerService = trainerService;
            this.trainingTypeService = trainingTypeService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("FetchAll");
        }

        public async Task<IActionResult> FetchAll()
        {
            var data = await service.FetchAllTrainingLists();
            return View(data);
        }

        public async Task<IActionResult> AddTrainingList()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainingList(TrainingListModal trainingList)
        {
            await service.AddTrainingList(trainingList);
            await LoadDropdowns();
            return RedirectToAction("FetchAll");          
            
        }

        public async Task<IActionResult> EditTrainingList(int id)
        {
            var data = await service.FindTrainingListByID(id);
            await LoadDropdowns();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrainingList(TrainingListModal trainingList)
        {
            await service.UpdateTrainingList(trainingList);
            await LoadDropdowns();
            return RedirectToAction("FetchAll");
     
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainingList(int id)
        {
            var data = await service.FindTrainingListByID(id);
            await service.DeleteTrainingList(id);
            return RedirectToAction("FetchAll");
        }
        public async Task LoadDropdowns()
        {
            var trainers = await trainerService.FetchAllTrainers();
            var trainingTypes = await trainingTypeService.FetchAllTrainingTypes();
            ViewBag.Trainers = new SelectList( trainers, "Id", "Fname");
            ViewBag.TrainingTypes = new SelectList(trainingTypes,"TrainingTypeId","TrainingTypeName");
        }
    }
}