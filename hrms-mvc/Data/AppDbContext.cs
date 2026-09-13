using hrms_mvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace hrms_mvc.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<DeductionType> DeductionTypes { get; set; }
        public DbSet<DepartmentLeaves> DepartmentLeaves { get; set; }
        public DbSet<Earning> Earning { get; set; }
        public DbSet<EarningType> EarningTypes { get; set; }
        public DbSet<EmployeeBankDetails> EmployeeBankDetails { get; set; }
        public DbSet<EmployeeDeductions> EmployeeDeductions { get; set; }
        public DbSet<EmployeeEarnings> EmployeeEarnings { get; set; }
        public DbSet<EmployeeSalaries> EmployeeSalaries { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<MasterLeaveType> MasterLeaveTypes { get; set; }
        public DbSet<Payslips> Payslips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DepartmentLeaves>()
                    .HasOne(dl => dl.MasterLeaveType)
                    .WithMany(mlt => mlt.DepartmentLeaves)
                    .HasForeignKey(dl => dl.LeaveTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
        }




    }

}
