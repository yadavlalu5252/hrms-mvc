using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
  public class LeaveService : ILeaveService
        {
            private readonly AppDbContext db;

            public LeaveService(AppDbContext db)
            {
                this.db = db;
            }

            public List<MasterLeaveType> GetLeaveTypes()
            {
                return db.MasterLeaveTypes
                    .OrderByDescending(x => x.Id)
                    .ToList();
            }

            public MasterLeaveType? GetLeaveType(int id)
            {
                return db.MasterLeaveTypes
                    .FirstOrDefault(x => x.Id == id);
            }

            public void AddLeaveType(MasterLeaveType leaveType)
            {
                db.MasterLeaveTypes.Add(leaveType);
                db.SaveChanges();
            }

            public void DeleteLeaveType(int id)
            {
                var leaveType = db.MasterLeaveTypes
                    .FirstOrDefault(x => x.Id == id);

                if (leaveType != null)
                {
                    db.MasterLeaveTypes.Remove(leaveType);
                    db.SaveChanges();
                }
            }

            public List<DepartmentLeaves> GetDepartmentLeaves()
            {
                return db.DepartmentLeaves
                    .Include(x => x.Department)
                    .Include(x => x.MasterLeaveType)
                    .OrderByDescending(x => x.Id)
                    .ToList();
            }

            public DepartmentLeaves? GetDepartmentLeave(int id)
            {
                return db.DepartmentLeaves
                    .Include(x => x.Department)
                    .Include(x => x.MasterLeaveType)
                    .FirstOrDefault(x => x.Id == id);
            }

            public void AddDepartmentLeave(DepartmentLeaves departmentLeave)
            {
                db.DepartmentLeaves.Add(departmentLeave);
                db.SaveChanges();
            }

            public void DeleteDepartmentLeave(int id)
            {
                var departmentLeave = db.DepartmentLeaves
                    .FirstOrDefault(x => x.Id == id);

                if (departmentLeave != null)
                {
                    db.DepartmentLeaves.Remove(departmentLeave);
                    db.SaveChanges();
                }
            }

            public List<Department> GetDepartments()
            {
                return db.Departments
                    .OrderBy(x => x.Name)
                    .ToList();
            }

            public void ChangeLeaveTypeStatus(int id, string status)
            {
                var leaveType = db.MasterLeaveTypes
                    .FirstOrDefault(x => x.Id == id);

                if (leaveType != null)
                {
                    leaveType.Status = status;
                    db.SaveChanges();
                }
            }
        }
    }
