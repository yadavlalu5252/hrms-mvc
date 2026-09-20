using hrms_mvc.Models;
using hrms_mvc.Repository;
using hrms_mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IDesignationService ds;
        private readonly IDepartmentService deptServ;
        public DesignationController(IDesignationService ds, IDepartmentService deptServ)
        {
            this.ds = ds;
            this.deptServ = deptServ;
        }

        public async Task<IActionResult> Index()
        {
            var allDesignation = await ds.GetAllDesignation();
            var allDepartment = await deptServ.GetAllDepartments();

            ViewBag.Departments = allDepartment;
            return View(allDesignation);
        }

        public async Task<IActionResult> AddDesignation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDesignation(Designation designation)
        {
            if (!ModelState.IsValid)
            {
                var allDesignation = await ds.GetAllDesignation();
                var allDepartment = await deptServ.GetAllDepartments();

                ViewBag.Departments = allDepartment;

                return View(designation);
            }

            designation.CreatedAt = DateTime.Now;
            designation.CreatedBy = HttpContext.Session.GetString("Role");

            await ds.AddDesignation(designation);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditDesignation(Designation designation)
        {
            if (!ModelState.IsValid)
            {
                var allDesignation = await ds.GetAllDesignation();
                var allDepartment = await deptServ.GetAllDepartments();

                ViewBag.Departments = allDepartment;

                return View("Index", allDesignation);
            }

            var oldDesignation = await ds.GetDesignationById(designation.Id);

            if (oldDesignation == null)
            {
                return NotFound();
            }

            oldDesignation.DesignationName = designation.DesignationName;
            oldDesignation.DepartmentId = designation.DepartmentId;
            oldDesignation.Status = designation.Status;
            oldDesignation.ModifiedAt = DateTime.Now;
            oldDesignation.ModifiedBy = HttpContext.Session.GetString("Role");

            await ds.UpdateDesignation(oldDesignation);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            await ds.DeleteDesignation(id);

            return RedirectToAction("Index");
        }
    }
}
