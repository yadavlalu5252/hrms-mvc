using hrms_mvc.Models;

namespace hrms_mvc.Repository
{
    public interface ITicketsService
    {
        Task<List<Tickets>> GetAllTickets();
        Task<Tickets?> GetTicketById(int id);
        Task<int> AddTicket(Tickets ticket);
        Task<int> UpdateTicket(Tickets ticket);
        Task<int> DeleteTicket(int id);
        Task<List<User>> GetAllUsers();
        Task<int> AddComment(TicketComment comment);
        Task<int> AddResolution(TicketResolution resolution);
        Task<int> AddAttachment(TicketAttachment attachment);
    }
}
