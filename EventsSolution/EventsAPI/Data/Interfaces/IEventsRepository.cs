using EventsAPI.Model;

namespace EventsAPI.Data.Interfaces
{
    public interface IEventsRepository
    {
        Task<List<Event>> GetAllEvents();
        Task<Event> GetEventById(Guid id);
        Task<bool> CreateEvent(Event evento);
        Task<bool> UpdateEvent(Event evento);
        Task<bool> DeleteEvent(Guid id);
    }
}
