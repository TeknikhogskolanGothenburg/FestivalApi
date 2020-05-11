using System;
using festival_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace festival_api.DB_Context
{
    public class FestivalDbContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public FestivalDbContext()
        {}

        public FestivalDbContext(IConfiguration config, DbContextOptions options): base(options)
        {
            _configuration = config;
        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Gig> Gigs { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("FestivalApiDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasData(new
                {
                    EventId = 1,
                    EventName = "Big Splash",
                    EventDate = new DateTime(2021, 06, 23, 19, 30, 00),
                    VenueId = 1
                });
            modelBuilder.Entity<Venue>()
                .HasData(new
                {
                    VenueId = 1,
                    VenueName = "Rock Hall",
                    VenueStreet = "123 Main Street",
                    VenueCity = "Du Bois",
                    VenueState = "PA",
                    VenueZip = "18702",
                    VenueCountry = "USA",
                    VenueSeats = 145,
                    ServesAlcohol = true
                });
            modelBuilder.Entity<Gig>()
                .HasData(new
                {
                    GigId = 1,
                    GigHeadline = "Rumble",
                    GigLengthInMinutes = 60,
                    EventId = 1,
                    ArtistId = 1

                }, new
                {
                    GigId = 2,
                    GigHeadline = "Boston Tea Party",
                    GigLengthInMinutes = 70,
                    EventId = 1,
                    ArtistId = 2

                });
            modelBuilder.Entity<Artist>()
                .HasData(new
                {
                    ArtistId = 1,
                    ArtistName = "C64's",
                    ArtistEmail = "c64@gmail.com",
                    ArtistPhone = "111-222-3333"
                }, new
                {
                    ArtistId = 2,
                    ArtistName = "Plasma Screen",
                    ArtistEmail = "plasma@gmail.com",
                    ArtistPhone = "444-555-6666"
                });
        }


    }
}