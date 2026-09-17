using hrms_mvc.Data;
using hrms_mvc.Models;
using Microsoft.EntityFrameworkCore;
using hrms_mvc.Repository.EventsRepo;

namespace hrms_mvc.Services.EventsService
{
    public class EventsService:IEvents
    {
        private readonly AppDbContext db;
        public EventsService(AppDbContext db)
        {
            this.db = db;
        }

        

        public async Task<List<EventModel>>GetEvents()
        {
            return await db.Events.ToListAsync();
        }

        

        public async Task AddEvent(EventModel model)
        {
            model.Status = "active";
            await db.Events.AddAsync(model);
            await db.SaveChangesAsync();
        }
    }
}
