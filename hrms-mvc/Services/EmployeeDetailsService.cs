using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class EmployeeDetailsService : IEmployeeDetailsService
    {
        private readonly AppDbContext db;
        public EmployeeDetailsService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<User?> GetEmployeeProfile(int id)
        {
            var data = await db.Users
         .Include(x=>x.Department)
         .Include(x=>x.Designation)
        .Include(x => x.EmployeeBankDetails)
        .Include(x => x.EmployeeFamilyDetails)
        .Include(x => x.EducationDetails)
        .Include(x => x.Experiences)
        .FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }

        public async Task<EmployeeBankDetails?> GetBankDetails(int userId)
        {
            var data = await db.EmployeeBankDetails.SingleOrDefaultAsync(x => x.UserId == userId);

            return data;

        }
        public async Task<int> AddBankDetails(EmployeeBankDetails bankDetails)
        {
            await db.EmployeeBankDetails.AddAsync(bankDetails);
            return await db.SaveChangesAsync();
        }
        public async Task<int> UpdateBankDetails(EmployeeBankDetails bankDetails)
        {
            db.EmployeeBankDetails.Update(bankDetails);
            return await db.SaveChangesAsync();
        }



        public async Task<List<EmployeeFamilyDetail>> GetFamilyDetails(int userId)
        {
            var data = await db.EmployeeFamilyDetails.Where(x=>x.UserId==userId).ToListAsync();
            return data;
        }
        public async Task<int> AddFamilyDetails(EmployeeFamilyDetail familyDetail)
        {
            await db.EmployeeFamilyDetails.AddAsync(familyDetail);
            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdateFamilyDetails(EmployeeFamilyDetail familyDetail)
        {
            db.EmployeeFamilyDetails.Update(familyDetail);
            return await db.SaveChangesAsync();
        }


        public async Task<List<EducationDetails>> GetEducationDetails(int userId)
        {
            return await db.EducationDetails
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }
        public async Task<int> AddEducationDetails(EducationDetails educationDetails)
        {
            db.EducationDetails.Add(educationDetails);

            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdateEducationDetails(EducationDetails educationDetails)
        {
            db.EducationDetails.Update(educationDetails);

            return await db.SaveChangesAsync();
        }

        public async Task<List<Experience>> GetExperienceDetails(int userId)
        {
            return await db.Experiences
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<int> AddExperienceDetails(Experience experience)
        {
            db.Experiences.Add(experience);

            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdateExperienceDetails(Experience experience)
        {
            db.Experiences.Update(experience);

            return await db.SaveChangesAsync();
        }
    }
}
