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
        public async Task<ActionResult<Event[]>> GetEvents(bool includeGigs = false)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEventById(int id, bool includeGigs=false)
        {
            try {
                var result =  await _eventRepository.GetEvent(id, includeGigs);
                return Ok(result);
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event e)
        {
            // OBS Funkar ännu inte fullt ut
            _eventRepository.Add(e);
            if(await _eventRepository.Save())
            {
                return Created("", e);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}