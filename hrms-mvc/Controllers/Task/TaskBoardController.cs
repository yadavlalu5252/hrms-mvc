using hrms_mvc.Repository.TaskRepo;
using Microsoft.AspNetCore.Mvc;

namespace hrms_mvc.Controllers.Tasks
{
    public class TaskBoardController : Controller
    {
        private readonly ITask taskService;

        public TaskBoardController(ITask taskService)
        {
            this.taskService = taskService;
        }


        
        [HttpGet]
        public async Task<IActionResult> TaskBoard()
        {
            var data = await taskService.GetTaskBoards();

            return View(
                "~/Views/Task/TaskBoard/TaskBoard.cshtml",
                data
            );
        }


        
        [HttpGet]
        public async Task<IActionResult> AddTaskBoard()
        {
            ViewBag.Projects = await taskService.GetProjects();

            return View(
                "~/Views/Task/TaskBoard/AddTaskBoard.cshtml"
            );
        }


        
        [HttpGet]
        public async Task<IActionResult> GetTasksByProject(int projectId)
        {
            var tasks =
                await taskService.GetTasksByProject(projectId);

            var data = tasks.Select(x => new
            {
                id = x.TasksId,
                title = x.Title
            });

            return Json(data);
        }


       
        [HttpPost]
        public async Task<IActionResult> AddTaskBoard(
            hrms_mvc.Models.TaskBoards model)
        {
            await taskService.AddTaskBoard(model);

            return RedirectToAction("TaskBoard");
        }
    }
}