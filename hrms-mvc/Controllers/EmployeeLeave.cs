using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class EmployeeLeaveController : Controller
    {
        private readonly ILeaveService service;

        public EmployeeLeaveController(ILeaveService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ViewMyLeaveRequests()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var leaveRequests =
                await service.GetMyLeaveRequests(
                    userId.Value);

            var leaveBalances =
                await service.GetMyLeaveBalances(
                    userId.Value);

            var leaveTypes =
                await service.GetAvailableLeaveTypes(
                    userId.Value);

            ViewBag.UserId =
                userId.Value;

            ViewBag.LeaveBalances =
                leaveBalances;

            ViewBag.LeaveTypes =
                leaveTypes;

            return View(leaveRequests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyLeave(
            LeaveRequest leaveRequest)
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            leaveRequest.UserId =
                userId.Value;

            ModelState.Remove("User");
            ModelState.Remove("MasterLeaveType");
            ModelState.Remove("ApprovedBy");
            ModelState.Remove("StatusHistory");

            leaveRequest.Status =
                "Pending";

            leaveRequest.NumberOfDays =
                (leaveRequest.EndDate -
                 leaveRequest.StartDate).Days + 1;

            if (ModelState.IsValid)
            {
                await service.AddLeaveRequest(
                    leaveRequest);

                TempData["success"] =
                    "Leave applied successfully!";

                return RedirectToAction(
                    "ViewMyLeaveRequests");
            }

            var leaveRequests =
                await service.GetMyLeaveRequests(
                    userId.Value);

            var leaveBalances =
                await service.GetMyLeaveBalances(
                    userId.Value);

            var leaveTypes =
                await service.GetAvailableLeaveTypes(
                    userId.Value);

            ViewBag.UserId =
                userId.Value;

            ViewBag.LeaveBalances =
                leaveBalances;

            ViewBag.LeaveTypes =
                leaveTypes;

            return View(
                "ViewMyLeaveRequests",
                leaveRequests);
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentLeaveDetails()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var departmentLeaves =
                await service.GetMyDepartmentLeaves(
                    userId.Value);

            return View(departmentLeaves);
        }
    }
}