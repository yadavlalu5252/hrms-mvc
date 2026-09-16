using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        private readonly IEmployeeDetailsService eds;
        public EmployeeDetailsController(IEmployeeDetailsService eds)
        {
            this.eds = eds;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var employee = await eds.GetEmployeeProfile(userId.Value);
            var bankDetails = await eds.GetBankDetails(userId.Value);
            var familyDetails = await eds.GetFamilyDetails(userId.Value);

            ViewBag.Employee = employee;
            ViewBag.BankDetails = bankDetails;
            ViewBag.FamilyDetails = familyDetails;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBankDetails(EmployeeBankDetails bankDetails)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            bankDetails.UserId = userId.Value;
            if (!ModelState.IsValid)
            {
                var employee = await eds.GetEmployeeProfile(userId.Value);
                var bankDetailss = await eds.GetBankDetails(userId.Value);
                var familyDetails = await eds.GetFamilyDetails(userId.Value);

                ViewBag.Employee = employee;
                ViewBag.BankDetails = bankDetailss;
                ViewBag.FamilyDetails = familyDetails;

                return View("Index");
            }
            await eds.AddBankDetails(bankDetails);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditBankDetails(EmployeeBankDetails bankDetails)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var oldBankDetails = await eds.GetBankDetails(userId.Value);

            if (oldBankDetails == null)
            {
                return NotFound();
            }

            oldBankDetails.BankName = bankDetails.BankName;
            oldBankDetails.AccountNumber = bankDetails.AccountNumber;
            oldBankDetails.IFSCCode = bankDetails.IFSCCode;
            oldBankDetails.BranchName = bankDetails.BranchName;

            await eds.UpdateBankDetails(oldBankDetails);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> AddFamilyDetails(EmployeeFamilyDetail familyDetail)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            familyDetail.UserId = userId.Value;
            if (!ModelState.IsValid)
            {
                var employee = await eds.GetEmployeeProfile(userId.Value);
                var bankDetailss = await eds.GetBankDetails(userId.Value);
                var familyDetails = await eds.GetFamilyDetails(userId.Value);

                ViewBag.Employee = employee;
                ViewBag.BankDetails = bankDetailss;
                ViewBag.FamilyDetails = familyDetails;

                return View("Index");
            }
            await eds.AddFamilyDetails(familyDetail);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditFamilyDetails(EmployeeFamilyDetail familyDetail)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var familyDetails = await eds.GetFamilyDetails(userId.Value);

            var oldFamilyDetail = familyDetails
                .FirstOrDefault(x => x.Id == familyDetail.Id);

            if (oldFamilyDetail == null)
            {
                return NotFound();
            }

            oldFamilyDetail.Name = familyDetail.Name;
            oldFamilyDetail.Relation = familyDetail.Relation;
            oldFamilyDetail.DateOfBirth = familyDetail.DateOfBirth;
            oldFamilyDetail.MobileNumber = familyDetail.MobileNumber;

            await eds.UpdateFamilyDetails(oldFamilyDetail);

            return RedirectToAction("Index");
        }
    }
}
