using System;
using System.Collections.Generic;

namespace festival_api.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public Venue Venue { get; set; }
        public ICollection<Gig> Gigs { get; set; }
    }
}