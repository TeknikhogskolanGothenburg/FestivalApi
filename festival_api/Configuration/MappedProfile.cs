using AutoMapper;
using festival_api.Dto;
using festival_api.Models;

namespace festival_api.Services
{
    public class MappedProfile: Profile
    {
        public MappedProfile()
        {
            CreateMap<Event, EventDto>()
                .ReverseMap()
                .ForMember(v => v.Venue, o => o.Ignore());

            CreateMap<Gig, GigDto>()
                .ReverseMap();

            CreateMap<Venue, VenueDto>()
                .ReverseMap();

            CreateMap<Artist, ArtistDto>()
                .ReverseMap();
        }
        
    }
}