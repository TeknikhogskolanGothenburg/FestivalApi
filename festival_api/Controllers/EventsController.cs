using System;
using System.Threading.Tasks;
using festival_api.Models;
using festival_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace festival_api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class EventsController: ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Event[]>> GetEvents([FromQuery]bool includeGigs = false)
        {
            try
            {
                var results = await _eventRepository.GetEvents(includeGigs);
                return Ok(results);
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
    }
}