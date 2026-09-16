using hrms_mvc.Models;
using hrms_mvc.Repository.EventsRepo;
using Microsoft.AspNetCore.Mvc;

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

            return View("~/Views/EventController/index11.cshtml", data);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(EventModel model)
        {
            await eventService.AddEvent(model);

            return RedirectToAction("index11");
        }

    }
}
