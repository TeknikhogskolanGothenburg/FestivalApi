using System;
using System.Linq;
using System.Threading.Tasks;
using festival_api.DB_Context;
using festival_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace festival_api.Services
{
    public class EventRepository : Repository, IEventRepository
    {
        public EventRepository(FestivalDbContext festivalContext, ILogger<EventRepository> logger): base(festivalContext, logger)
        {}

        public async Task<Event> GetEvent(int eventId, bool includeGigs = false)
        {
            _logger.LogInformation($"Getting event with id {eventId}");
            IQueryable<Event> query = _festivalContext.Events
                .Include(v => v.Venue);
            if(includeGigs)
            {
                query = query.Include(g => g.Gigs)
                        .ThenInclude(a => a.Artist);
            }
            query = query.Where(e => e.EventId == eventId);
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<Event[]> GetEvents(bool includeGigs = false)
        {
            
            _logger.LogInformation("Getting events");
            IQueryable<Event> query = _festivalContext.Events
                .Include(v => v.Venue);
            
            if(includeGigs)
            {
                query = query.Include(g => g.Gigs)
                        .ThenInclude(a => a.Artist);
            }
            query = query.OrderBy(e => e.EventDate);
            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetEventsByDate(DateTime date, bool includeGigs = false)
        {
             _logger.LogInformation("Getting events");
            IQueryable<Event> query = _festivalContext.Events
                .Include(v => v.Venue);
            
            if(includeGigs)
            {
                query = query.Include(g => g.Gigs)
                        .ThenInclude(a => a.Artist);
            }
            query = query.OrderBy(e => e.EventDate)
                .Where(e => e.EventDate == date);

            return await query.ToArrayAsync();
        }
        
    }
}