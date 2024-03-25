using MongoDB.Driver;

namespace EventsAPI.Data.Interfaces
{
    public interface IMongoDbConnection
    {
        IMongoDatabase GetDatabase();

    }
}
