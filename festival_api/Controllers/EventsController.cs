using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using festival_api.Dto;
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
        private readonly IMapper _mapper; 

        public EventsController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<ActionResult<EventDto[]>> GetEvents(bool includeGigs = false)
        {
            try
            {
                var results = await _eventRepository.GetEvents(includeGigs);
                var mappedResults = _mapper.Map<EventDto[]>(results);
                return Ok(mappedResults);
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEventById(int id, bool includeGigs=false)
        {
            try {
                var result =  await _eventRepository.GetEvent(id, includeGigs);
                var mappedResult = _mapper.Map<EventDto>(result);
                return Ok(mappedResult);
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<EventDto[]>> GetEventsByDate(DateTime date, bool includeGigs=false)
        {
            try {
                var results =  await _eventRepository.GetEventsByDate(date, includeGigs);
                if(!results.Any()) 
                {
                    return NotFound();
                }

                var mappedResults = _mapper.Map<EventDto[]>(results);
                return Ok(mappedResults);
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<EventDto>> PostEvent(EventDto eventDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Event>(eventDto);

                _eventRepository.Add(mappedEntity);
                if(await _eventRepository.Save())
                {
                    return Created($"/api/v1.0/events/{mappedEntity.EventId}", _mapper.Map<EventDto>(mappedEntity));
                }
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }

        [HttpPut("{eventId}")]
        public async Task<ActionResult> PutEvent(int eventId, EventDto eventDto)
        {
            try
            {
                var oldEvent = await _eventRepository.GetEvent(eventId);
                if(oldEvent == null)
                {
                    return NotFound($"Counld not find event with id {eventId}");
                }   

                var newEvent = _mapper.Map(eventDto, oldEvent);
                _eventRepository.Update(newEvent);
                if(await _eventRepository.Save())
                {
                    return NoContent();
                }
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }

        [HttpDelete("{eventId}")]
        public async Task<ActionResult> DeleteEvent(int eventId)
        {
            try
            {
                var oldEvent = await _eventRepository.GetEvent(eventId);
                if(oldEvent == null)
                {
                    return NotFound($"Counld not find event with id {eventId}");
                }  
                _eventRepository.Delete(oldEvent);
                if(await _eventRepository.Save())
                {
                    return NoContent();
                }
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }


    }
}