using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;


namespace hrms_mvc.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IPromotionService ps;
        public PromotionController(IPromotionService ps)
        {
            this.ps = ps;
        }

        public async Task<IActionResult> Index()
        {
            var allPromotions = await ps.GetAllPromotions();
            ViewBag.Users = await ps.GetAllUsers();
            ViewBag.Designations = await ps.GetAllDesignations();

            return View(allPromotions);
        }

        public async Task<IActionResult> AddPromotion()
        {
            ViewBag.Users = await ps.GetAllUsers();
            ViewBag.Designations = await ps.GetAllDesignations();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPromotion(Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await ps.GetAllUsers();
                ViewBag.Designations = await ps.GetAllDesignations();

                return View(promotion);
            }

            await ps.AddPromotion(promotion);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditPromotion(Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                var allPromotions = await ps.GetAllPromotions();
                ViewBag.Users = await ps.GetAllUsers();
                ViewBag.Designations = await ps.GetAllDesignations();

                return View("Index", allPromotions);
            }

            var oldPromotion = await ps.GetPromotionById(promotion.PromotionId);

            if (oldPromotion == null)
            {
                return NotFound();
            }

            oldPromotion.UserId = promotion.UserId;
            oldPromotion.DesignationFrom = promotion.DesignationFrom;
            oldPromotion.DesignationTo = promotion.DesignationTo;
            oldPromotion.Date = promotion.Date;

            await ps.UpdatePromotion(oldPromotion);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            await ps.DeletePromotion(id);
            return RedirectToAction("Index");
        }
    }
}