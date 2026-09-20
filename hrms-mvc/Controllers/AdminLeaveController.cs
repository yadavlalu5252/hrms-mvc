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



        public async Task<IActionResult> ManageLeaveType()
        {
            var leaveTypes =await service.GetLeaveTypes();

            return View(leaveTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLeaveType( MasterLeaveType leaveType)
        {
            ModelState.Remove("DepartmentLeaves");
            ModelState.Remove("LeaveBalances");
            ModelState.Remove("LeaveRequests");

            leaveType.Status = "Active";

            if (ModelState.IsValid)
            {
                await service.AddLeaveType(leaveType);

                TempData["success"] = "Leave type added successfully!";

                return RedirectToAction(
                    nameof(ManageLeaveType));
            }

            var leaveTypes =
                await service.GetLeaveTypes();

            return View(
                nameof(ManageLeaveType),
                leaveTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLeaveType(
            MasterLeaveType leaveType)
        {
            ModelState.Remove("DepartmentLeaves");
            ModelState.Remove("LeaveBalances");
            ModelState.Remove("LeaveRequests");

            if (ModelState.IsValid)
            {
                await service.UpdateLeaveType(leaveType);

                TempData["success"] =
                    "Leave type updated successfully!";

                return RedirectToAction(
                    nameof(ManageLeaveType));
            }

            var leaveTypes =
                await service.GetLeaveTypes();

            return View(
                nameof(ManageLeaveType),
                leaveTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLeaveType(
            int id)
        {
            await service.DeleteLeaveType(id);

            TempData["success"] =
                "Leave type deleted successfully!";

            return RedirectToAction(
                nameof(ManageLeaveType));
        }



        public async Task<IActionResult> ManageLeaveSettings()
        {
            var leaveTypes =
                await service.GetLeaveTypes();

            return View(leaveTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageLeaveStatus(
            int id,
            string status)
        {
            await service.ChangeLeaveTypeStatus(
                id,
                status);

            TempData["success"] =
                "Leave status updated successfully!";

            return RedirectToAction(
                nameof(ManageLeaveSettings));
        }



        [HttpGet]
        public async Task<IActionResult> AddLeaveDeptwise()
        {
            ViewBag.Departments =
                await service.GetDepartments();

            ViewBag.LeaveTypes =
                await service.GetLeaveTypes();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLeaveDeptwise(
     DepartmentLeaves departmentLeave)
        {
            ModelState.Remove("Department");
            ModelState.Remove("MasterLeaveType");
            ModelState.Remove("LeaveBalances");
            ModelState.Remove("Status");

            departmentLeave.Status = "Active";

            if (ModelState.IsValid)
            {
                await service.AddDepartmentLeave(
                    departmentLeave);

                TempData["success"] =
                    "Leave allocated successfully!";

                return RedirectToAction(
                    "DepartmentLeaveDetails");
            }

            ViewBag.Departments =
                await service.GetDepartments();

            ViewBag.LeaveTypes =
                await service.GetLeaveTypes();

            return View(departmentLeave);
        }
        [HttpGet]
        public async Task<IActionResult> DepartmentLeaveDetails()
        {
            var departmentLeaves =
                await service.GetDepartmentLeaves();

            return View(departmentLeaves);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDepartmentLeave(
            int id)
        {
            await service.DeleteDepartmentLeave(id);

            TempData["success"] = "Department leave deleted successfully!";

            return RedirectToAction(nameof(DepartmentLeaveDetails));
        }
    }
}