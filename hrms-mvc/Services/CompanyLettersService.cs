using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class CompanyLettersService : ICompanyLetters
    {
        private readonly AppDbContext db;

        public CompanyLettersService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Documents>> FetchCompanyLetters(string email)
        {
            var data = await db.Documents
                .Where(x => x.Email == email &&
                            x.DocName.Contains("Letter"))
                .ToListAsync();

            return data;
        }
    }
}