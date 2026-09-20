using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class MyDocumentService : IMyDocument
    {
        private readonly AppDbContext db;

        public MyDocumentService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Documents>> FetchMyDocuments(string email)
        {
            var data = await db.Documents
                .Where(x => x.Email == email)
                .ToListAsync();

            return data;
        }
    }
}