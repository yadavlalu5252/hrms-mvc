using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IEmployeeUploadDocument
    {
        Task SaveDocument(EmployeeDocumentUploadViewModel model,string email);
    }
}