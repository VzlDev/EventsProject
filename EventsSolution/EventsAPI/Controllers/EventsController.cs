using EventsAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventsAPI.Data;
using EventsAPI.Model;

namespace EventsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        protected IEventsRepository _repository;

        public EventsController(IEventsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetEvents()
        {
            var events = await _repository.GetAllEvents();
            return events;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var evento = await _repository.GetEventById(id);
            return evento;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateEvent(Event evento)
        {
            if (evento == null)
            {
                return BadRequest();
            }
            
            return await _repository.CreateEvent(evento);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteEvent(Guid id)
        {
            return await _repository.DeleteEvent(id);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateEvents(Event evento)
        {
            if (evento == null)
            {
                return NotFound();
            }

            return await _repository.UpdateEvent(evento);
        }

        [HttpPut("{eventId}/users/add/{userId}")]
        public async Task<ActionResult<bool>> AddUserToEvent(Guid userId, Guid eventId)
        {
            Event evento = await _repository.GetEventById(eventId);

            if (evento == null)
            {
                return NotFound();
            }

            evento.Users.Add(userId);

            return await _repository.UpdateEvent(evento);
        }

        [HttpPut("{eventId}/users/remove/{userId}")]
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
