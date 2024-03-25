using EventsAPI.Data.Interfaces;
using EventsAPI.Model;
using MongoDB.Driver;

namespace EventsAPI.Data
{
    public class EventsRepository : IEventsRepository
    {
        private readonly IMongoDbConnection _connection;
        private readonly IMongoCollection<Event> _collection;

        public EventsRepository(IMongoDbConnection connection)
        {
            _connection = connection;

            var database = _connection.GetDatabase();
            _collection = database.GetCollection<Event>("events");
        }

        public async Task<bool> CreateEvent(Event evento)
        {
            try
            {
                await _collection.InsertOneAsync(evento);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEvent(Guid id)
        {
            //Guid resultGuid = Guid.Parse(id);

            var filterDefinition = Builders<Event>.Filter.Eq(a => a.EventId, id);
            var result = await _collection.DeleteOneAsync(filterDefinition);
            return result.DeletedCount > 0;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _collection.Find(Builders<Event>.Filter.Empty).ToListAsync();
        }

        public async Task<Event> GetEventById(Guid id)
        {
            //Guid resultGuid = Guid.Parse(id);

            var filterDefinition = Builders<Event>.Filter.Eq(a => a.EventId, id);
            return await _collection.Find(filterDefinition).FirstAsync();
        }

        public async Task<bool> UpdateEvent(Event evento)
        {
            var filterDefinition = Builders<Event>.Filter.Eq(a => a.EventId, evento.EventId);
            var result = await _collection.ReplaceOneAsync(filterDefinition, evento);
            return result.ModifiedCount > 0;
        }
    }
}
