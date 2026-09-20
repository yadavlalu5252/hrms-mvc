using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class EmployeeAttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;

        public EmployeeAttendanceController(
            IAttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var attendanceList =
                await attendanceService
                    .GetAttendanceByUserId(userId.Value);

            var todayAttendance =
                await attendanceService
                    .GetTodayAttendance(userId.Value);

            ViewBag.TodayAttendance =
                todayAttendance;

            ViewBag.UserId =
                userId.Value;

            return View(attendanceList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            await attendanceService
                .CheckIn(userId.Value);

            TempData["success"] =
                "Checked in successfully!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LunchOut()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            await attendanceService
                .LunchOut(userId.Value);

            TempData["success"] =
                "Lunch out recorded successfully!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LunchIn()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            await attendanceService
                .LunchIn(userId.Value);

            TempData["success"] =
                "Lunch in recorded successfully!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            await attendanceService
                .CheckOut(userId.Value);

            TempData["success"] =
                "Checked out successfully!";

            return RedirectToAction("Index");
        }
    }
}