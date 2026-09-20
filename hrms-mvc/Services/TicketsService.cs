using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly AppDbContext db;

        public TicketsService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Tickets>> GetAllTickets()
        {
            var data = await db.Tickets.Include(x => x.RaisedByUser)
                .Include(x => x.AssignedToUser)
                .Include(x => x.AssignedByUser)
                .Include(x => x.Comments)
                .ThenInclude(x => x.CommentByUser)
                .Include(x => x.Resolution)
                .Include(x => x.Attachments)
                .ToListAsync();

            return data;
        }


        public async Task<Tickets?> GetTicketById(int id)
        {
            var data = await db.Tickets.Include(x => x.RaisedByUser)
                .Include(x => x.AssignedToUser)
                .Include(x => x.AssignedByUser)
                .Include(x => x.Comments)
                .ThenInclude(x => x.CommentByUser)
                .Include(x => x.Resolution)
                .Include(x => x.Attachments)
                .SingleOrDefaultAsync(x => x.TicketId == id);

            return data;
        }

        public async Task<int> AddTicket(Tickets ticket)
        {
            await db.Tickets.AddAsync(ticket);
            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdateTicket(Tickets ticket)
        {
            db.Tickets.Update(ticket);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteTicket(int id)
        {
            var ticket = await db.Tickets.SingleOrDefaultAsync(x => x.TicketId == id);

            if (ticket == null)
            {
                return 0;
            }

            db.Tickets.Remove(ticket);
            return await db.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var data = await db.Users.ToListAsync();
            return data;
        }

        public async Task<int> AddComment(TicketComment comment)
        {
            await db.TicketComments.AddAsync(comment);
            return await db.SaveChangesAsync();
        }

        public async Task<int> AddResolution(TicketResolution resolution)
        {
            await db.TicketResolutions.AddAsync(resolution);
            return await db.SaveChangesAsync();
        }

        public async Task<int> AddAttachment(TicketAttachment attachment)
        {
            await db.TicketAttachments.AddAsync(attachment);
            return await db.SaveChangesAsync();
        }
    }
} 