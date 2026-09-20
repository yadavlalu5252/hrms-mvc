using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IMyDocument
    {
        Task<List<Documents>> FetchMyDocuments(string email);
    }
}