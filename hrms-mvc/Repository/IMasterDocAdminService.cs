using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IMasterDocAdminService
    {
        Task<List<MasterDocAdmin>> FetchAllAdminDocuments();

        Task<MasterDocAdmin> FindAdminDocumentByID(int id);

        Task AddAdminDocument(MasterDocAdmin adminDocument);

        Task UpdateAdminDocument(MasterDocAdmin adminDocument);

        Task DeleteAdminDocument(int id);
    }
}