using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext db;

        public AuthService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<User?> Login(string email, string password)
        {
            User? user = await db.Users
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Email == email &&
                                           x.Password == password);

            return user;
        }
    }
}