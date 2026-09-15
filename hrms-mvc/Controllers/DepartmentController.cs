using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentService ds;
        public DepartmentController(IDepartmentService ds)
        {
            this.ds = ds;
        }

        public async Task<IActionResult> Index()
        {
            var allDepartmenet = await ds.GetAllDepartments();
            return View(allDepartmenet);
        }

        public async Task<IActionResult> AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            department.CreatedAt = DateTime.Now;
            department.CreatedBy = HttpContext.Session.GetString("Role");

            await ds.AddDepartment(department);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                var allDepartment = await ds.GetAllDepartments();

                return View("Index", allDepartment);
            }

            var oldDepartment = await ds.GetDepartmentById(department.Id);

            if (oldDepartment == null)
            {
                return NotFound();
            }

            oldDepartment.Name = department.Name;
            oldDepartment.Status = department.Status;
            oldDepartment.ModifiedAt = DateTime.Now;
            oldDepartment.ModifiedBy = HttpContext.Session.GetString("Role");

            await ds.UpdateDepartment(oldDepartment);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await ds.DeleteDepartment(id);

            return RedirectToAction("Index");
        }


    }
}
