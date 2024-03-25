using EventsAPI.Data.Interfaces;
using EventsAPI.Model;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EventsAPI.Health
{
    public class EventsHealthCheck : IHealthCheck
    {
        private readonly IMongoDbConnection _connection;

        public EventsHealthCheck(IMongoDbConnection connection)
        {
            _connection = connection;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var db = _connection.GetDatabase();
                var collection = db.GetCollection<Event>("events");

                if (db != null && collection != null)
                {
                    return Task.FromResult(HealthCheckResult.Healthy("Events database connection is healthy."));
                }

                return Task.FromResult(HealthCheckResult.Unhealthy($"Events database connection is unhealthy"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"Events database connection is unhealthy: {ex.Message}"));
            }
        }
    }
}
