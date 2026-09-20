using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IAuthService
    {
         Task<User?> Login(string email, string password);
        Task<User?> LoginWithGoogle(string email);
    }
}
