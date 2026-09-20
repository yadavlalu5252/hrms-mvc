
using hrms_mvc.Models;


namespace hrms_mvc.Repository.ProjectsRepo


{
    public interface IProjects
    {
        Task<List<Projects>> Fectchall();

    Task AddProject(Projects model);

        Task <List<User>>fetchmanager();

        Task<List<User>> fetchusers();

        Task deleteinfo (int id);

        //Task updateinfo(Projects model);
        Task updateinfo(int id, Projects model);

    }
}
