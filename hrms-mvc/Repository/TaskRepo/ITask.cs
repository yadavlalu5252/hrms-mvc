using hrms_mvc.Models;

namespace hrms_mvc.Repository.TaskRepo
{
    public interface ITask
    {
        Task<List<Tasks>> GetTasks();

        Task<List<Projects>> GetProjects();

        Task AddTask(Tasks model);

        Task<List<TaskBoards>> GetTaskBoards();

        Task AddTaskBoard(TaskBoards model);

        Task<List<Tasks>> GetTasksByProject(int projectId);
    }
}