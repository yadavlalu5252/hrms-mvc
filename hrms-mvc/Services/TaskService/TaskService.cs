using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository.TaskRepo;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services.TaskService
{
    public class TaskService : ITask
    {
        private readonly AppDbContext db;

        public TaskService(AppDbContext db)
        {
            this.db = db;
        }


        
        public async Task<List<Tasks>> GetTasks()
        {
            return await db.Tasks
                .Include(x => x.Project)
                .ToListAsync();
        }


        
        public async Task<List<Projects>> GetProjects()
        {
            return await db.AllProjects
                .ToListAsync();
        }


        
        public async Task AddTask(Tasks model)
        {
            await db.Tasks.AddAsync(model);

            await db.SaveChangesAsync();
        }

        
        public async Task<List<TaskBoards>> GetTaskBoards()
        {
            return await db.TaskBoards
                .Include(x => x.Project)
                .Include(x => x.Task)
                .ToListAsync();
        }


        public async Task AddTaskBoard(TaskBoards model)
        {
            await db.TaskBoards.AddAsync(model);

            await db.SaveChangesAsync();
        }


        
        public async Task<List<Tasks>> GetTasksByProject(int projectId)
        {
            return await db.Tasks
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();
        }
    }
}