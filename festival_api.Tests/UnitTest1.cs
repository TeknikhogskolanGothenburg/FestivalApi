using System;
using System.Collections;
using System.Collections.Generic;
using festival_api.DB_Context;
using festival_api.Models;
using festival_api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace festival_api.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            // Arrange
            IList<Event> events = GenerateEvents();
            var festivalContextMock = new Mock<FestivalDbContext>();
            festivalContextMock.Setup(e => e.Events).ReturnsDbSet(events);

            var logger = Mock.Of<ILogger<EventRepository>>();
            var eventRepository = new EventRepository(festivalContextMock.Object, logger);
            // Act
            var theEvents = await eventRepository.GetEvents();

            // Assert
            Assert.Equal(1, (int)theEvents.Length);
        }

        [Fact]
        public async void Test2()
        {
            // Arrange
            IList<Event> events = GenerateEvents();
            var festivalContextMock = new Mock<FestivalDbContext>();
            festivalContextMock.Setup(e => e.Events).ReturnsDbSet(events);

            var logger = Mock.Of<ILogger<EventRepository>>();
            var eventRepository = new EventRepository(festivalContextMock.Object, logger);
            // Act
            var theEvent = await eventRepository.GetEvent(1);

            // Assert
            Assert.Equal(1, theEvent.EventId);
        }

        private static IList<Event> GenerateEvents()
        {
            return new List<Event>
                {
                    new Event
                    {
                        EventId = 1,
                        EventName = "The Fun Thing",
                        EventDate = new DateTime(2021, 10, 23, 18, 20, 0),
                        Venue = new Venue
                            {
                                VenueId = 1,
                                VenueName = "Homestead",
                                VenueStreet = "331 Main Street",
                                VenueCity = "Hometown",
                                VenueState = "None",
                                VenueZip = "123445",
                                ServesAlcohol = true
                            },
                        Gigs = null
                    }
                };
        }
    }
}
