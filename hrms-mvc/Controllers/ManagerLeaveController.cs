using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class ManagerLeaveController : Controller
    {
        private readonly ILeaveService service;

        public ManagerLeaveController(ILeaveService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> LeaveRequests()
        {
            var managerId =
                HttpContext.Session.GetInt32("UserId");

            if (managerId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var leaveRequests =
                await service.GetManagerLeaveRequests(
                    managerId.Value);

            return View(leaveRequests);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var managerId =
                HttpContext.Session.GetInt32("UserId");

            if (managerId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            await service.ApproveLeave(
                id,
                "Manager");

            TempData["success"] =
                "Leave approved successfully!";

            return RedirectToAction(
                "LeaveRequests");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var managerId =
                HttpContext.Session.GetInt32("UserId");

            if (managerId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            await service.RejectLeave(
                id,
                "Manager");

            TempData["success"] =
                "Leave rejected successfully!";

            return RedirectToAction(
                "LeaveRequests");
        }
    }
}