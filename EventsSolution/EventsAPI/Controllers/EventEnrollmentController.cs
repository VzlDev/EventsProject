using Confluent.Kafka;
using EventsAPI.Data.Interfaces;
using EventsAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventEnrollmentController : ControllerBase
    {
        protected IEventsRepository _repository;
        private readonly IProducer<string, string> _producer;

        public EventEnrollmentController(IEventsRepository repository)
        {
            _repository = repository;
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        [HttpPut("{eventId}/register")]
        public async Task<ActionResult<bool>> AddUserToEvent(Guid userId, Guid eventId)
        {
            Event evento = await _repository.GetEventById(eventId);

            if (evento == null)
            {
                return NotFound();
            }

            evento.Users.Add(userId);

            LogEntry log = new LogEntry()
            {
                Id = Guid.NewGuid(),
                EventTitle = evento.Title,
                UserId = userId,
                RegistrationTime = DateTime.UtcNow,
            };

            await _producer.ProduceAsync("user-event-registrations", new Message<string, string>
            {
                Key = "UserEventRegistered",
                Value = JsonConvert.SerializeObject(log)
            });

            return await _repository.UpdateEvent(evento);
        }

        [HttpPut("{eventId}/cancel")]
        public async Task<ActionResult<bool>> RemoveUserToEvent(Guid userId, Guid eventId)
        {
            Event evento = await _repository.GetEventById(eventId);

            if (evento == null)
            {
                return NotFound();
            }

            evento.Users.Remove(userId);

            return await _repository.UpdateEvent(evento);
        }
    }
}
