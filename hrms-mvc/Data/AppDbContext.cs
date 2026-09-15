using hrms_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<EventModel> Events { get; set; }
        public DbSet<EventTypes> EventsTypes { get; set; }
        public DbSet<Projects> AllProjects { get; set; }
        public DbSet<TaskBoards> TaskBoards { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskMembers> Taskmembers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskBoards>()
                .HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskBoards>()
                .HasOne(x => x.Task)
                .WithMany()
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}