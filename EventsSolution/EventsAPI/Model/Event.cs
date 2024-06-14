using EventsAPI.DTO;
using MongoDB.Bson.Serialization.Attributes;
using UsersAPI.Model;

namespace EventsAPI.Model
{
    public class Event
    {
        [BsonId]
        public Guid EventId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("users")]
        public List<Guid> Users { get; set; } // List of users associated with the event

        public Event(EventDTO dTO)
        {
            this.EventId = Guid.NewGuid();
            this.Title = dTO.Title;
            this.Description = dTO.Description;
            this.Location = dTO.Location;
            this.Date = dTO.Date;
            this.Users = dTO.Users;
        }

        public Event() { }
    }

}
