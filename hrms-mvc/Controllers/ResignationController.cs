using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class ResignationController : Controller
    {
        private readonly IResignationService rs;
        public ResignationController(IResignationService rs)
        {
            this.rs = rs;
        }

        public async Task<IActionResult> Index()
        {
            var allResignations = await rs.GetAllResignations();
            return View(allResignations);
        }

        public async Task<IActionResult> AddResignation()
        {
            ViewBag.Users = await rs.GetAllUsers();
            ViewBag.Departments = await rs.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddResignation(Resignation resignation)
        {
           if (resignation.ResignDate < resignation.NoticeDate)
            {
                ModelState.AddModelError("ResignDate", "Resignation date cannot be earlier than notice date");
            }

             if (!ModelState.IsValid)
            {
                ViewBag.Users = await rs.GetAllUsers();
                ViewBag.Departments = await rs.GetAllDepartments();

                return View(resignation);
            }

            await rs.AddResignation(resignation);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditResignation(Resignation resignation)
        {
            if (resignation.ResignDate < resignation.NoticeDate)
            {
                ModelState.AddModelError("ResignDate", "Resignation date cannot be earlier than notice date");
            }

            if (!ModelState.IsValid)
            {
                var allResignations = await rs.GetAllResignations();
                ViewBag.Users = await rs.GetAllUsers();
                ViewBag.Departments = await rs.GetAllDepartments();

                return View("Index", allResignations);
            }

            var oldResignation = await rs.GetResignationById(resignation.ResignationId);

            if (oldResignation == null)
            {
                return NotFound();
            }

            oldResignation.UserId = resignation.UserId;
            oldResignation.DepartmentId = resignation.DepartmentId;
            oldResignation.NoticeDate = resignation.NoticeDate;
            oldResignation.ResignDate = resignation.ResignDate;
            oldResignation.Reason = resignation.Reason;

            await rs.UpdateResignation(oldResignation);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteResignation(int id)
        {
            await rs.DeleteResignation(id);
            return RedirectToAction("Index");
        }
    }
}