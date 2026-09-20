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
      int LeaveTypeId,
      DateTime StartDate,
      DateTime EndDate,
      string Reason)
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            if (LeaveTypeId <= 0)
            {
                TempData["error"] =
                    "Please select a leave type.";

                return RedirectToAction(
                    "ViewMyLeaveRequests");
            }

            if (StartDate == default ||
                EndDate == default)
            {
                TempData["error"] =
                    "Please select start date and end date.";

                return RedirectToAction(
                    "ViewMyLeaveRequests");
            }

            if (EndDate < StartDate)
            {
                TempData["error"] =
                    "End date cannot be before start date.";

                return RedirectToAction(
                    "ViewMyLeaveRequests");
            }

            if (string.IsNullOrWhiteSpace(Reason))
            {
                TempData["error"] =
                    "Please enter a reason for leave.";

                return RedirectToAction(
                    "ViewMyLeaveRequests");
            }

            var leaveRequest = new LeaveRequest
            {
                UserId = userId.Value,
                LeaveTypeId = LeaveTypeId,
                StartDate = StartDate,
                EndDate = EndDate,
                NumberOfDays =
                    (EndDate - StartDate).Days + 1,
                Reason = Reason,
                Status = "Pending",
                ApprovedBy = "",
                StatusHistory = ""
            };

            await service.AddLeaveRequest(
                leaveRequest);

            TempData["success"] =
                "Leave applied successfully!";

            return RedirectToAction(
                "ViewMyLeaveRequests");
        }

      
    }
}