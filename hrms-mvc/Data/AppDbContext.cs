using hrms_mvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace hrms_mvc.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
