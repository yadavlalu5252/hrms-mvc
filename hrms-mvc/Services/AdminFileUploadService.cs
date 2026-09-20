using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;

namespace hrms_mvc.Services
{
    public class AdminFileUploadService : IAdminFileUpload
    {
        private readonly AppDbContext db;

        public AdminFileUploadService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task SaveFile(AdminFileUploadViewModel model)
        {
            string uploadFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads"
            );

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
                    Email = model.Email,
                    DocName = model.DocumentName,
                    DocFile = fileName
                };

                await db.Documents.AddAsync(data);

                await db.SaveChangesAsync();
            }
        }
    }
}