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
            var educationDetails = await eds.GetEducationDetails(userId.Value);
            var experienceDetails = await eds.GetExperienceDetails(userId.Value);

            ViewBag.Employee = employee;
            ViewBag.BankDetails = bankDetails;
            ViewBag.FamilyDetails = familyDetails;
            ViewBag.EducationDetails = educationDetails;
            ViewBag.ExperienceDetails = experienceDetails;

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
                await LoadEmployeeDetails(userId.Value);
                return View("Index");
            }

            var existingBankDetails = await eds.GetBankDetails(userId.Value);

            if (existingBankDetails != null)
            {
                existingBankDetails.BankName = bankDetails.BankName;
                existingBankDetails.AccountNumber = bankDetails.AccountNumber;
                existingBankDetails.IFSCCode = bankDetails.IFSCCode;
                existingBankDetails.BranchName = bankDetails.BranchName;

                await eds.UpdateBankDetails(existingBankDetails);
            }
            else
            {
                await eds.AddBankDetails(bankDetails);
            }

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
                var bankDetails = await eds.GetBankDetails(userId.Value);
                var familyDetails = await eds.GetFamilyDetails(userId.Value);
                var educationDetails = await eds.GetEducationDetails(userId.Value);
                var experienceDetails = await eds.GetExperienceDetails(userId.Value);

                ViewBag.Employee = employee;
                ViewBag.BankDetails = bankDetails;
                ViewBag.FamilyDetails = familyDetails;
                ViewBag.EducationDetails = educationDetails;
                ViewBag.ExperienceDetails = experienceDetails;

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
        [HttpPost]
        public async Task<IActionResult> AddEducationDetails(
            EducationDetails educationDetails)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            educationDetails.UserId = userId.Value;

            if (!ModelState.IsValid)
            {
                await LoadEmployeeDetails(userId.Value);
                return View("Index");
            }

            await eds.AddEducationDetails(educationDetails);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditEducationDetails(
            EducationDetails educationDetails)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var educationDetailsList =
                await eds.GetEducationDetails(userId.Value);

            var oldEducationDetails = educationDetailsList
                .FirstOrDefault(x =>
                    x.EducationDetailsId ==
                    educationDetails.EducationDetailsId);

            if (oldEducationDetails == null)
            {
                return NotFound();
            }

            oldEducationDetails.EducationType =
                educationDetails.EducationType;

            oldEducationDetails.UniversityName =
                educationDetails.UniversityName;

            oldEducationDetails.startdate =
                educationDetails.startdate;

            oldEducationDetails.enddate =
                educationDetails.enddate;

            await eds.UpdateEducationDetails(oldEducationDetails);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddExperienceDetails(
            Experience experience)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            experience.UserId = userId.Value;

            if (!ModelState.IsValid)
            {
                await LoadEmployeeDetails(userId.Value);
                return View("Index");
            }

            await eds.AddExperienceDetails(experience);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditExperienceDetails(
            Experience experience)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var experienceDetails =
                await eds.GetExperienceDetails(userId.Value);

            var oldExperience = experienceDetails
                .FirstOrDefault(x =>
                    x.ExperienceId == experience.ExperienceId);

            if (oldExperience == null)
            {
                return NotFound();
            }

            oldExperience.CompanyName = experience.CompanyName;
            oldExperience.DesignationName = experience.DesignationName;
            oldExperience.FromDate = experience.FromDate;
            oldExperience.ToDate = experience.ToDate;

            await eds.UpdateExperienceDetails(oldExperience);

            return RedirectToAction("Index");
        }

        private async Task LoadEmployeeDetails(int userId)
        {
            var employee = await eds.GetEmployeeProfile(userId);
            var bankDetails = await eds.GetBankDetails(userId);
            var familyDetails = await eds.GetFamilyDetails(userId);
            var educationDetails = await eds.GetEducationDetails(userId);
            var experienceDetails = await eds.GetExperienceDetails(userId);

            ViewBag.Employee = employee;
            ViewBag.BankDetails = bankDetails;
            ViewBag.FamilyDetails = familyDetails;
            ViewBag.EducationDetails = educationDetails;
            ViewBag.ExperienceDetails = experienceDetails;
        }
    }
}
