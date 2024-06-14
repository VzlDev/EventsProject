using LogsAPI.Data;
using LogsAPI.Model;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LogsAPI.Services
{
    public class LogService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<LogService> _logger;

        public LogService(IServiceProvider serviceProvider, ILogger<LogService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

            var config = new ConsumerConfig
            {
                GroupId = "log-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("user-event-registrations");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    var registration = JsonConvert.DeserializeObject<LogEntry>(consumeResult.Message.Value);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<LogContext>();

                        context.LogEntries.Add(registration);
                        await context.SaveChangesAsync(stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing Kafka message.");
                }
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }
}
