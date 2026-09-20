using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IAdminFileUpload
    {
        Task SaveFile(AdminFileUploadViewModel model);
    }
}