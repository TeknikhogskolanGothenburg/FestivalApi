namespace festival_api.Models
{
    public class Gig
    {
        public int GigId { get; set; }
        public string GigHeadline { get; set; }
        public int GigLengthInMinutes { get; set; }
        public Event Event { get; set; }
        public Artist Artist { get; set; }
        
    }
}