using hrms_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet <DepartmentLeaves> DepartmentLeaves { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmployeeBankDetails> EmployeeBankDetails { get; set; }
        public DbSet<EmployeeFamilyDetail> EmployeeFamilyDetails { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<EventModel> Events { get; set; }
        public DbSet<EventTypes> EventsTypes { get; set; }
        public DbSet<Projects> AllProjects { get; set; }
        public DbSet<TaskBoards> TaskBoards { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskMembers> Taskmembers { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<DeductionType> DeductionTypes { get; set; }
        public DbSet<Earning> Earning { get; set; }
        public DbSet<EarningType> EarningTypes { get; set; }
        public DbSet<EmployeeDeductions> EmployeeDeductions { get; set; }
        public DbSet<EmployeeEarnings> EmployeeEarnings { get; set; }
        public DbSet<EmployeeSalaries> EmployeeSalaries { get; set; }
        public DbSet<Payslips> Payslips { get; set; }

   
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<MasterLeaveType> MasterLeaveTypes { get; set; }


        public DbSet<Promotion> Promotions { get; set; } 
        public DbSet<Resign> Resigns { get; set; }
        public DbSet<Resignation> Resignations { get; set; }
        public DbSet<Termination> Terminations { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<TicketResolution> TicketResolutions { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<EducationDetails> EducationDetails { get; set; }
        public DbSet<Experience> Experiences { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany()
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Designation)
                .WithMany()
                .HasForeignKey(u => u.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Designation>()
                .HasOne(d => d.Department)
                .WithMany(dep => dep.Designations)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<DepartmentLeaves>()
                .HasOne(dl => dl.MasterLeaveType)
                .WithMany(mlt => mlt.DepartmentLeaves)
                .HasForeignKey(dl => dl.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasOne(x => x.EmployeeBankDetails)
            .WithOne(x => x.User)
            .HasForeignKey<EmployeeBankDetails>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Promotion>()
               .HasOne(p => p.User)
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resignation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resignation>()
                .HasOne(r => r.Department)
                .WithMany()
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Termination>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.RaisedByUser)
                .WithMany()
                .HasForeignKey(t => t.RaisedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.AssignedToUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.AssignedByUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketComment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketComment>()
                .HasOne(c => c.CommentByUser)
                .WithMany()
                .HasForeignKey(c => c.CommentBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketResolution>()
                .HasOne(r => r.Ticket)
                .WithOne(t => t.Resolution)
                .HasForeignKey<TicketResolution>(r => r.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketResolution>()
                .HasOne(r => r.ResolvedByUser)
                .WithMany()
                .HasForeignKey(r => r.ResolvedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketAttachment>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketAttachment>()
                .HasOne(a => a.UploadedByUser)
                .WithMany()
                .HasForeignKey(a => a.UploadedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EducationDetails>()
                .HasOne(x => x.User)
                .WithMany(x => x.EducationDetails)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Timesheet>()
            .HasOne(t => t.Projects)
            .WithMany()
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Experience>()
            .HasOne(x => x.User)
            .WithMany(x => x.Experiences)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}