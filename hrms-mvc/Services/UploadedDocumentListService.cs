using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class UploadedDocumentListService : IUploadedDocument
    {
        private readonly AppDbContext db;

        public UploadedDocumentListService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Documents>> FetchAllFiles()
        {
            var data = await db.Documents.ToListAsync();

            return data;
        }

        public async Task<Documents?> FindById(int id)
        {
            var data = await db.Documents
                .FirstOrDefaultAsync(x => x.DocId == id);

            return data;
        }

        public async Task DeleteFile(int id)
        {
            var file = await db.Documents.FindAsync(id);

            if (file != null)
            {
                string filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    file.DocFile
                );

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                db.Documents.Remove(file);

                await db.SaveChangesAsync();
            }
        }
    }
}