using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IUploadedDocument
    {
        Task<List<Documents>> FetchAllFiles();

        Task<Documents?> FindById(int id);

        Task DeleteFile(int id);
    }
}