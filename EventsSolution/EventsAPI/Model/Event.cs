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
    }

}
