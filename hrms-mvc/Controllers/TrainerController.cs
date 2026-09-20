using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService service;

        public TrainerController(ITrainerService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction("FetchAll");
        }

        public async Task<IActionResult> FetchAll()
        {
            var data = await service.FetchAllTrainers();
            return View(data);
        }

        public IActionResult AddTrainers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainers(Trainers trainer)
        {
            if (trainer.imagename != null)
            {
                trainer.ProfilePicture = trainer.imagename.FileName;
            }
            else
            {
                trainer.ProfilePicture = "default.jpg";
            }

            ModelState.Remove("ProfilePicture");         
            await service.AddTrainers(trainer);
            return RedirectToAction("FetchAll");
           
        }

        public async Task<IActionResult> EditTrainer(int id)
        {
            var data = await service.FindTrainersByID(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTrainer(Trainers trainer)
        {           
         await service.UpdateTrainers(trainer);
         return RedirectToAction("FetchAll");
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var data = await service.FindTrainersByID(id);
            await service.DeleteTrainers(id);
            return RedirectToAction("FetchAll");
        }
    }
}