using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AdminTimesheetController : Controller
    {
        private readonly ITimesheetService service;

        public AdminTimesheetController(
            ITimesheetService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> AdminTimesheet()
        {
            var timesheets =
                await service.GetTimesheets();

            return View(timesheets);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Employees =
                await service.GetEmployees();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Timesheet timesheet)
        {
            ModelState.Remove("User");

            timesheet.Status = "Pending";
            timesheet.CreatedBy = "Admin";
            timesheet.CreatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                await service.AddTimesheet(timesheet);

                TempData["success"] =
                    "Timesheet added successfully!";

                return RedirectToAction("AdminTimesheet");
            }

            ViewBag.Employees =
                await service.GetEmployees();

            return View(timesheet);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var timesheet =
                await service.GetTimesheet(id);

            if (timesheet == null)
            {
                return NotFound();
            }

            ViewBag.Employees =
                await service.GetEmployees();

            return View(timesheet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Timesheet timesheet)
        {
            ModelState.Remove("User");

            if (id != timesheet.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await service.UpdateTimesheet(timesheet);

                TempData["success"] =
                    "Timesheet updated successfully!";

                return RedirectToAction("AdminTimesheet");
            }

            ViewBag.Employees =
                await service.GetEmployees();

            return View(timesheet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteTimesheet(id);

            TempData["success"] =
                "Timesheet deleted successfully!";

            return RedirectToAction("AdminTimesheet");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveSelected(
            int[] selectedIds)
        {
            if (selectedIds.Length > 0)
            {
                await service.ApproveTimesheets(selectedIds);

                TempData["success"] =
                    "Selected timesheets approved successfully!";
            }

            return RedirectToAction("AdminTimesheet");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectSelected(
            int[] selectedIds)
        {
            if (selectedIds.Length > 0)
            {
                await service.RejectTimesheets(selectedIds);

                TempData["success"] =
                    "Selected timesheets rejected successfully!";
            }

            return RedirectToAction("AdminTimesheet");
        }
    }
}