using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface IMasterDocEmpService
    {
        Task<List<MasterDocEmp>> FetchAllEmpDoc();

        Task<MasterDocEmp> FindEmpDocByID(int id);

        Task AddEmpDoc(MasterDocEmp empDoc);

        Task UpdateEmpDoc(MasterDocEmp empDoc);

        Task DeleteEmpDoc(int id);
    }
}