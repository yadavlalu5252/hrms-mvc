using hrms_mvc.Models;
using hrms_mvc.Repository.EventsRepo;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace hrms_mvc.Controllers.Events
{
    public class EventController : Controller
    {
        
       
            private readonly IEvents eventService;

            public EventController(IEvents eventService)
            {
                this.eventService = eventService;
            }


        public async Task<IActionResult> index11()
        {
            var data = await eventService.GetEvents();
            ViewBag.EventTypes = await eventService.GetMasterEvent();

            return View("~/Views/EventController/index11.cshtml", data);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(EventModel model)
        {
            await eventService.AddEvent(model);

            return RedirectToAction("index11");
        }

        [HttpPost]
        public async Task<IActionResult>AddMasterEvent(EventTypes master)
        {
            await eventService.AddMasterEvent(master);
            return RedirectToAction("MasterEvent");

        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteMasterEvent(int id)
        {
            await eventService.DeleteMasterEvent(id);

            return RedirectToAction("MasterEvent");
        }



        public async Task<IActionResult> MasterEvent()
        {
            var data = await eventService.GetMasterEvent();
            return View("~/Views/EventController/MasterEvent.cshtml", data);
        }

        
        public async Task<IActionResult>GetData()
        {
            var data = await eventService.GetEvent();
            return View("~/Views/EventController/GetData.cshtml",data);
        }

        
        [HttpPost]
        public async Task<IActionResult> EditEvent(EventModel model)
        {
            await eventService.UpdateEvent(model);

            return RedirectToAction("GetData");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await eventService.DeleteEvent(id);

            return RedirectToAction("GetData");
        }
        
    }
}
