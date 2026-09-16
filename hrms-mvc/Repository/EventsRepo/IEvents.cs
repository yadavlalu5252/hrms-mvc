using hrms_mvc.Models;

namespace hrms_mvc.Repository.EventsRepo
{
    public interface IEvents
    {

        // Task<EventModel> AddDate();

        ///Task<EventModel> GetDate();

        //Task<EventModel> UpdateDate();

        //Task<EventModel>DeleteDate();
        Task<List<EventModel>> GetEvents();

        Task AddEvent(EventModel model);

        //Task EditEvent(EventModel model);

        //Task DeleteEvent(int id);

        //Task<int> AddEvents(EventTypes addeventtype);
        //Task <List<EventTypes>> FetchEvents(EventTypes fetcheventtype);
        //Task<EventModel> GetEvents();
        //Task<EventModel> UpdateEevents();
        //Task<EventModel> DeleteEvents();

    }
}
