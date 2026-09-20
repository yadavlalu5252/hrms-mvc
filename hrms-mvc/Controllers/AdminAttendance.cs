using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AdminAttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;

        public AdminAttendanceController(
            IAttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> AdminAttendanceList()
        {
            var attendanceList =
                await attendanceService.GetAttendanceList();

            ViewBag.Employees =
                await attendanceService.GetEmployees();

            return View(attendanceList);
        }

        [HttpGet]
        public async Task<IActionResult> AttendanceByUser(int userId)
        {
            var attendanceList =
                await attendanceService.GetAttendanceByUserId(userId);

            ViewBag.Employees =
                await attendanceService.GetEmployees();

            return View("AdminAttendanceList", attendanceList);
        }

        [HttpGet]
        public async Task<IActionResult> FilterAttendance(
            DateTime? startDate,
            DateTime? endDate)
        {
            var attendanceList =
                await attendanceService.GetAttendanceByDate(
                    startDate,
                    endDate);

            ViewBag.Employees =
                await attendanceService.GetEmployees();

            return View("AdminAttendanceList", attendanceList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var attendance =
                await attendanceService.GetAttendanceById(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var attendance =
                await attendanceService.GetAttendanceById(id);

            if (attendance == null)
            {
                return NotFound();
            }

            ViewBag.Employees =
                await attendanceService.GetEmployees();

            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await attendanceService.UpdateAttendance(
                    attendance);

                TempData["success"] =
                    "Attendance updated successfully!";

                return RedirectToAction(
                    nameof(AdminAttendanceList));
            }

            ViewBag.Employees =
                await attendanceService.GetEmployees();

            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await attendanceService.DeleteAttendance(id);

            TempData["success"] =
                "Attendance deleted successfully!";

            return RedirectToAction(
                nameof(AdminAttendanceList));
        }
    }
}