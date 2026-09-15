using hrms_mvc.Models;
using hrms_mvc.Repository;
using hrms_mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class EmployeeListController : Controller
    {
        private readonly IEmployeeListService els;
        private readonly IRoleService rs;
        private readonly IDesignationService dgs;
        private readonly IDepartmentService ds;
        public EmployeeListController(IEmployeeListService els, IRoleService rs, IDesignationService dgs, IDepartmentService ds)
        {
            this.els = els;
            this.rs = rs;
            this.dgs = dgs;
            this.ds = ds;
        }

        public async Task<IActionResult> Index()
        {
            var allRoles = await rs.GetAllRole();
            var allDesignation = await dgs.GetAllDesignation();
            var allDepartment = await ds.GetAllDepartments();
            var allEmployeeList = await els.GetAllEmployees();

            ViewBag.Roles = allRoles;
            ViewBag.Departments = allDepartment;
            ViewBag.Designations = allDesignation;
            
            return View(allEmployeeList);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeList(User user)
        {
            if (!ModelState.IsValid)
            {
                var allRoles = await rs.GetAllRole();
                var allDesignation = await dgs.GetAllDesignation();
                var allDepartment = await ds.GetAllDepartments();
                var allEmployeeList = await els.GetAllEmployees();

                ViewBag.Roles = allRoles;
                ViewBag.Departments = allDepartment;
                ViewBag.Designations = allDesignation;

                return View(allEmployeeList);
            }

            user.CreatedAt = DateTime.Now;
            user.CreatedBy = HttpContext.Session.GetString("Role");

            await els.AddEmployee(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(User user)
        {
            if (!ModelState.IsValid)
            {
                var allRoles = await rs.GetAllRole();
                var allDesignation = await dgs.GetAllDesignation();
                var allDepartment = await ds.GetAllDepartments();
                var allEmployeeList = await els.GetAllEmployees();

                ViewBag.Roles = allRoles;
                ViewBag.Departments = allDepartment;
                ViewBag.Designations = allDesignation;

                return View("Index", allEmployeeList);
            }

            var oldEmp = await els.FindEmployeeById(user.Id);

            if (oldEmp == null)
            {
                return NotFound();
            }

            oldEmp.ProfilePicture = user.ProfilePicture;
            oldEmp.FirstName = user.FirstName;
            oldEmp.LastName = user.LastName;
            oldEmp.Email = user.Email;
            oldEmp.Password = user.Password;
            oldEmp.DateOfJoining = user.DateOfJoining;
            oldEmp.DateOfBirth = user.DateOfBirth;
            oldEmp.RoleId = user.RoleId;
            oldEmp.DepartmentId = user.DepartmentId;
            oldEmp.ReportingManager = user.ReportingManager;
            oldEmp.DesignationId = user.DesignationId;
            oldEmp.MobileNumber = user.MobileNumber;
            oldEmp.Address = user.Address;
            oldEmp.Gender = user.Gender;
            oldEmp.Status = user.Status;
            oldEmp.AboutEmployee = user.AboutEmployee;
            oldEmp.ModifiedAt = DateTime.Now;
            oldEmp.ModifiedBy = HttpContext.Session.GetString("Role");

            await els.UpdateEmployee(oldEmp);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployeeList(int id)
        {
            await els.DeleteEmployee(id);

            return RedirectToAction("Index");
        }
    }
}
