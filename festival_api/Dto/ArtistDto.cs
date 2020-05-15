using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using festival_api.Models;

namespace festival_api.Dto
{
    public class ArtistDto
    {
     
        [Required]
        public int ArtistId { get; set; }
        [Required]
        public string ArtistName { get; set; }
        [EmailAddress]
        public string ArtistEmail { get; set; }
        [Phone]
        public string ArtistPhone { get; set; }
        public ICollection<GigDto> Gigs { get; set; }   
    }
}