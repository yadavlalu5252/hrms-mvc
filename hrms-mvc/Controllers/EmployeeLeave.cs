using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILeaveService service;

        public LeaveController(
            ILeaveService service)
        {
            this.service = service;
        }



        [HttpGet]
        public async Task<IActionResult> ViewMyLeaveRequests(
            int userId)
        {
            var leaveRequests =
                await service.GetMyLeaveRequests(userId);

            var leaveBalances =
                await service.GetMyLeaveBalances(userId);

            var leaveTypes =
                await service.GetAvailableLeaveTypes();

            ViewBag.UserId =
                userId;

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
                    "ViewMyLeaveRequests",
                    new
                    {
                        userId =
                            leaveRequest.UserId
                    });
            }


            var leaveRequests =
                await service.GetMyLeaveRequests(
                    leaveRequest.UserId);

            var leaveBalances =
                await service.GetMyLeaveBalances(
                    leaveRequest.UserId);

            var leaveTypes =
                await service.GetAvailableLeaveTypes();


            ViewBag.UserId =
                leaveRequest.UserId;

            ViewBag.LeaveBalances =leaveBalances;

            ViewBag.LeaveTypes = leaveTypes;


            return View(
                "ViewMyLeaveRequests",
                leaveRequests);
        }
    }
}