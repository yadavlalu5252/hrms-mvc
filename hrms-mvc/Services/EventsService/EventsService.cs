using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository.EventsRepo;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;

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

        
        public async Task AddMasterEvent(EventTypes master)
        {
            await db.EventsTypes.AddAsync(master);
            await db.SaveChangesAsync();

        }

       
        public async Task DeleteMasterEvent(int id)
        {
            var data = await db.EventsTypes.FindAsync(id);

            if (data != null)
            {
                db.EventsTypes.Remove(data);

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<EventTypes>>GetMasterEvent()
        {
           return await db.EventsTypes.ToListAsync();
            

        }
        
        public async Task<List<EventModel>>GetEvent()
        {
            return await db.Events.ToListAsync();

        }
        
        public async Task DeleteEvent(int id)
        {

            var data = await db.Events.FindAsync(id);

            if (data != null)
            {
                db.Events.Remove(data);

                await db.SaveChangesAsync();
            }
        }
        
        public async Task UpdateEvent(EventModel model)
        {
            var data = await db.Events.FindAsync(model.EventModelId);

            if (data != null)
            {
                data.Title = model.Title;
                data.Date = model.Date;
                data.EventTypeId = model.EventTypeId;

                await db.SaveChangesAsync();
            }
        }

        
        
    }
}
