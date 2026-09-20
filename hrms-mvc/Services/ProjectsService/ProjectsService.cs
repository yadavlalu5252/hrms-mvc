using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository.EventsRepo;
using hrms_mvc.Repository.ProjectsRepo;
using Microsoft.EntityFrameworkCore;
namespace hrms_mvc.Services.ProjectsService
{

        public class ProjectsService : IProjects
        {
            private readonly AppDbContext db;
            public ProjectsService(AppDbContext db)
            {
                this.db = db;
            }

            public async Task AddProject(Projects model)
            {
                model.FilePath = "No File";
                model.LogoPath = "No Logo";
                model.Status = "Active";
                await db.AllProjects.AddAsync(model);
                await db.SaveChangesAsync();
            }

            public async Task<List<Projects>> Fectchall()
            {
                return await db.AllProjects.ToListAsync();
            }
        public async Task<List<User>> fetchmanager()
        {
            return await db.Users.
                Where(x => x.RoleId == 2).ToListAsync();
        }


        public async Task<List<User>> fetchusers()
        {
            return await db.Users.
                Where(x => x.RoleId == 3).ToListAsync();
        }


        public async Task deleteinfo(int id)
        {
            var data = await db.AllProjects.FindAsync(id);

            if (data != null)
            {
                db.AllProjects.Remove(data);

                await db.SaveChangesAsync();
            }
        }


        public async Task updateinfo(int id, Projects model)
        {
            var data = await db.AllProjects.FindAsync(id);

            if (data != null)
            {
                data.ProjectName = model.ProjectName;
                data.ClientName = model.ClientName;
                data.Description = model.Description;
                data.StartDate = model.StartDate;
                data.EndDate = model.EndDate;
                data.Priority = model.Priority;
                data.ProjectValue = model.ProjectValue;
                data.PriceType = model.PriceType;
                data.Status = model.Status;
                data.ManagerName = model.ManagerName;

                await db.SaveChangesAsync();
            }
        }
    }
}
