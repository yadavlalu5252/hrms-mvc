using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ICompanyLetters
    {
        Task<List<Documents>> FetchCompanyLetters(string email);
    }
}