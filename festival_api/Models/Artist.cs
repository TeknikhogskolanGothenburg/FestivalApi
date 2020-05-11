using System.Collections.Generic;

namespace festival_api.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistEmail { get; set; }
        public string ArtistPhone { get; set; }
        public ICollection<Gig> Gigs { get; set; }
    }
}