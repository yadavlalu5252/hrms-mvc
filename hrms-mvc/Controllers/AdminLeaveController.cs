using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AdminLeaveController : Controller
    {
        private readonly ILeaveService service;

        public AdminLeaveController(ILeaveService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult ManageLeaveType()
        {
            var leaveTypes = service.GetLeaveTypes();

            return View(leaveTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLeaveType(MasterLeaveType leaveType)
        {
            if (ModelState.IsValid)
            {
                leaveType.Status = "Active";

                service.AddLeaveType(leaveType);

                return RedirectToAction("ManageLeaveType");
            }

            var leaveTypes = service.GetLeaveTypes();

            return View("ManageLeaveType", leaveTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLeaveType(int id)
        {
            service.DeleteLeaveType(id);

            return RedirectToAction("ManageLeaveType");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageLeaveStatus(int id, string status)
        {
            service.ChangeLeaveTypeStatus(id, status);

            return RedirectToAction("ManageLeaveType");
        }
    }
}