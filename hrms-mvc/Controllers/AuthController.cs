using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            HttpContext.Session.SetString(
                "Name",
                loginUser.FirstName
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

        [HttpGet]
        public IActionResult GoogleLogin()
        {
            AuthenticationProperties properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", "Auth")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            AuthenticateResult result =
                await HttpContext.AuthenticateAsync("GoogleCookie");

            if (!result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            string? email = result.Principal?.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login");
            }

            User? user = await ac.LoginWithGoogle(email);

            if (user == null)
            {
                TempData["Error"] = "This email is not registered in HRMS.";
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetInt32(
                "UserId",
                user.Id
            );

            HttpContext.Session.SetString(
                "Email",
                user.Email!
            );

            HttpContext.Session.SetString(
                "Role",
                user.Role!.RoleName!
            );

            if (user.Role!.RoleName == "Admin")
            {
                return RedirectToAction(
                    "Index",
                    "Admin"
                );
            }
            else if (user.Role.RoleName == "Manager")
            {
                return RedirectToAction(
                    "Index",
                    "Manager"
                );
            }
            else if (user.Role.RoleName == "Employee")
            {
                return RedirectToAction(
                    "Index",
                    "Employee"
                );
            }

            HttpContext.Session.Clear();

            TempData["Error"] = "Invalid role assigned to this user.";

            return RedirectToAction("Login");
        }
        public  IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Login","Auth");
        }
    }
}
