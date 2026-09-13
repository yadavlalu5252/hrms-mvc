using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService ac;
        public AuthController(IAuthService ac)
        {
            this.ac = ac;
        }


        public async Task<IActionResult> Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(User u)
        {
            User? loginUser = await ac.Login(u.Email!, u.Password!);

            if (loginUser == null)
            {
                ViewBag.ErrorMessage = "Invalid email or password";

                return View(u);
            }

            HttpContext.Session.SetInt32(
                "UserId",
                loginUser.Id
            );

            HttpContext.Session.SetString(
                "Email",
                loginUser.Email!
            );

            HttpContext.Session.SetString(
                "Role",
                loginUser.Role!.RoleName!
            );

            if (loginUser.Role!.RoleName == "Admin")
            {
                return RedirectToAction(
                    "Index",
                    "Admin"
                );
            }
            else if (loginUser.Role.RoleName == "Manager")
            {
                return RedirectToAction(
                    "Index",
                    "Manager"
                );
            }
            else if (loginUser.Role.RoleName == "Employee")
            {
                return RedirectToAction(
                    "Index",
                    "Employee"
                );
            }

            HttpContext.Session.Clear();

            ViewBag.ErrorMessage = "Invalid role assigned to this user.";

            return View(u);
        }
        

    public  IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Login","Auth");
        }
    }
}
