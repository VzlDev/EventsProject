using MongoDB.Bson.Serialization.Attributes;

namespace EventsAPI.DTO
{
    public class EventDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime Date { get; set; }

        public List<Guid> Users { get; set; } // List of users associated with the event
    }
}
