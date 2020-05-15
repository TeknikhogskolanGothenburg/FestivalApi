using System.ComponentModel.DataAnnotations;
using festival_api.Models;

namespace festival_api.Dto
{
    public class GigDto
    {
        [Required]
        public int GigId { get; set; }
        [Required]
        public string GigHeadline { get; set; }
        [Range(20, 120)]
        public int GigLengthInMinutes { get; set; }
        public EventDto Event { get; set; }
        public ArtistDto Artist { get; set; }
    }
}