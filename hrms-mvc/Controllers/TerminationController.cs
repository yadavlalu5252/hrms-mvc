using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class TerminationController : Controller
    {
        private readonly ITerminationService ts;
        public TerminationController(ITerminationService ts)
        {
            this.ts = ts;
        }

        public async Task<IActionResult> Index()
        {
            var allTerminations = await ts.GetAllTerminations();
            return View(allTerminations);
        }

        public async Task<IActionResult> AddTermination()
        {
            ViewBag.Users = await ts.GetAllUsers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTermination(Termination termination)
        {
            if (termination.ResignDate < termination.NoticeDate)
            {
                ModelState.AddModelError("ResignDate", "Resignation date can't be earlier than notice date");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Users = await ts.GetAllUsers();

                return View(termination);
            }

            await ts.AddTermination(termination);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditTermination(Termination termination)
        {
            if (termination.ResignDate < termination.NoticeDate)
            {
                ModelState.AddModelError("ResignDate", "Resignation date cannot be earlier than notice date.");
            }

            if (!ModelState.IsValid)
            {
                var allTerminations = await ts.GetAllTerminations();
                ViewBag.Users = await ts.GetAllUsers();

                return View("Index", allTerminations);
            }

            var oldTermination = await ts.GetTerminationById(termination.TerminationId);

            if (oldTermination == null)
            {
                return NotFound();
            }

            oldTermination.UserId = termination.UserId;
            oldTermination.TerminationType = termination.TerminationType;
            oldTermination.NoticeDate = termination.NoticeDate;
            oldTermination.ResignDate = termination.ResignDate;
            oldTermination.Reason = termination.Reason;


            await ts.UpdateTermination(oldTermination);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTermination(int id)
        {
            await ts.DeleteTermination(id);
            return RedirectToAction("Index");
        }
    }
}