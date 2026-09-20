using hrms_mvc.Repository.TaskRepo;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers.Tasks
{
    public class TaskController : Controller
    {
        private readonly ITask taskService;

        public TaskController(ITask taskService)
        {
            this.taskService = taskService;
        }


        
        public async Task<IActionResult> Index()
        {
            var tasks = await taskService.GetTasks();

            ViewBag.Projects = await taskService.GetProjects();

            return View(tasks);
        }


        
        [HttpPost]
        public async Task<IActionResult> AddTask(
            hrms_mvc.Models.Tasks model)
        {
            await taskService.AddTask(model);

            return RedirectToAction("Index");
        }
    }
}