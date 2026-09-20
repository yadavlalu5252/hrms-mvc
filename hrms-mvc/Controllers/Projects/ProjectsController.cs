using hrms_mvc.Models;
using hrms_mvc.Repository.EventsRepo;
using hrms_mvc.Repository.ProjectsRepo;
using hrms_mvc.Services.EventsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;

namespace hrms_mvc.Controllers.Projects
{
    public class ProjectsController : Controller
    {
        private readonly IProjects projectService;
        

        public ProjectsController(IProjects projectService)
        {
            this.projectService = projectService;
        }
        

        [HttpPost]
        public async Task<IActionResult> AddProject(hrms_mvc.Models.Projects model)
        {
            await projectService.AddProject(model);
            return RedirectToAction("GetProject");
        }


        public async Task<IActionResult> GetProject()
        {
            
            var data = await projectService.Fectchall();

            
            ViewBag.Managers = await projectService.fetchmanager();

           
            ViewBag.Users = await projectService.fetchusers();

         
            return View(data);
        }
        //public async Task deleteinfo(int id)
        //{
        //    var data = await db.Users.FindAsync(id);

        //    if (data != null)
        //    {
        //        db.Users.Remove(data);

        //        await db.SaveChangesAsync();
        //    }
        //}
        // UPDATE
        


        // DELETE
        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await userRepository.deleteinfo(id);

        //    return RedirectToAction("GetProject");
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await projectService.deleteinfo(id);

            return RedirectToAction("GetProject");
        }




        //public async Task updateinfo(User user)
        //{
        //    var data = await db.Users.FindAsync(user.Id);

        //    if (data != null)
        //    {
        //        data.Name = user.Name;
        //        data.Email = user.Email;
        //        data.Mobile = user.Mobile;

        //        await db.SaveChangesAsync();
        //    }

        //}
        //[HttpPost]
        //public async Task<IActionResult> Update(User user)
        //{
        //    await userRepository.updateinfo(user);

        //    return RedirectToAction("GetProject");
        //}
        //[HttpPost]
        //public async Task<IActionResult> Update(hrms_mvc.Models.Projects model)
        //{
        //    await projectService.updateinfo(model);

        //    return RedirectToAction("GetProject");
        [HttpPost]
        public async Task<IActionResult> Update(int id, hrms_mvc.Models.Projects model)
        {
            await projectService.updateinfo(id, model);

            return RedirectToAction("GetProject");
        }
    }

    }
