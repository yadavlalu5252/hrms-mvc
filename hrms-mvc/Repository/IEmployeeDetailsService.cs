using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IEmployeeDetailsService
    {
        Task<User?> GetEmployeeProfile(int id);

        Task<EmployeeBankDetails?> GetBankDetails(int userId);
        Task<int> AddBankDetails(EmployeeBankDetails bankDetails);
        Task<int> UpdateBankDetails(EmployeeBankDetails bankDetails);

        Task<List<EmployeeFamilyDetail>> GetFamilyDetails(int userId);
        Task<int> AddFamilyDetails(EmployeeFamilyDetail familyDetail);
        Task<int> UpdateFamilyDetails(EmployeeFamilyDetail familyDetail);
    }
}
