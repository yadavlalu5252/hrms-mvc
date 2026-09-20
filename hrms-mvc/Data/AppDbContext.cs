using hrms_mvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace hrms_mvc.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TrainingType> TrainingType { get; set; }
        public DbSet<Traininglist> Traininglist { get; set; }

        public DbSet<Trainers> Trainers { get; set; }
        public DbSet<MasterDocAdmin> MasterDocAdmin { get; set; }
        public DbSet<MasterDocEmp> MasterDocEmp { get; set; }
        public DbSet<Documents> Documents { get; set; }

    }
}
