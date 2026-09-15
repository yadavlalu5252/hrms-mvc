using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService rs;
        public RoleController(IRoleService rs)
        {
            this.rs = rs;
        }


        public async Task<IActionResult> Index()
        {
            var allRoles = await rs.GetAllRole();
            return View(allRoles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }
            role.CreatedAt = DateTime.Now;
            role.CreatedBy = HttpContext.Session.GetString("Role");

            await rs.AddRole(role);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                var allRole = await rs.GetAllRole();

                return View("Index", allRole);
            }

            var oldRole = await rs.GetRoleById(role.Id);

            if (oldRole == null)
            {
                return NotFound();
            }

            oldRole.RoleName = role.RoleName;
            oldRole.Status = role.Status;
            oldRole.ModifiedAt = DateTime.Now;
            oldRole.ModifiedBy = HttpContext.Session.GetString("Role");

            await rs.UpdateRole(oldRole);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await rs.DeleteRole(id);

            return RedirectToAction("Index");
        }
    }
}
