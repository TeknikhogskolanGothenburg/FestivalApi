using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace festival_api.Dto
{
    public class EventDto
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        [StringLength(20)]
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public int VenueId { get; set; }
        public VenueDto Venue { get; set; }
        public ICollection<GigDto> Gigs { get; set; }   
    }
}