using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;

namespace hrms_mvc.Services
{
    public class EmployeeUploadDocumentService : IEmployeeUploadDocument
    {
        private readonly AppDbContext db;

        public EmployeeUploadDocumentService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task SaveDocument(EmployeeDocumentUploadViewModel model,string email)
        {
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","uploads" );

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            if (model.File != null)
            {
                string fileName = model.File.FileName;

                string filePath = Path.Combine(
                    uploadFolder,
                    fileName
                );

                using (FileStream stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                Documents data = new Documents
                {
                    Email = email,
                    DocName = model.DocumentName,
                    DocFile = fileName
                };

                await db.Documents.AddAsync(data);

                await db.SaveChangesAsync();
            }
        }
    }
}