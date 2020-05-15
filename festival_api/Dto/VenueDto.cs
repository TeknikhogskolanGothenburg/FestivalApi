using System.ComponentModel.DataAnnotations;

namespace festival_api.Dto
{
    public class VenueDto
    {
       // [Required]
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string VenueStreet { get; set; }
        public string VenueCity { get; set; }
        public string VenueState { get; set; }
        public string VenueZip { get; set; }
        public string VenueCountry { get; set; }
        public int VenueSeats { get; set; }
        public bool ServesAlcohol { get; set; }
    }
}