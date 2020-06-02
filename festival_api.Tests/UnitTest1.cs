using System;
using System.Collections;
using System.Collections.Generic;
using festival_api.DB_Context;
using festival_api.Dto;
using festival_api.Models;
using festival_api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using festival_api.Controllers;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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


        [Fact]
        public async void TestPost()
        {
            // Arrange
            var profile = new MappedProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(r => r.Add<Event>(It.IsAny<Event>()));
            eventRepoMock.Setup(r => r.GetEvents(It.IsAny<Boolean>())).Returns(Task.FromResult(new Event[1]));
            eventRepoMock.Setup(r => r.Save()).Returns(Task.FromResult(true));

            var controller = new EventsController(eventRepoMock.Object, mapper);
            var dto = new EventDto
            {
                EventName = "Jaha",
                EventDate = new DateTime(2020, 10, 22),
                VenueId = 1
            };

            // Act
            var result = await controller.PostEvent(dto);
            
            // Assert
            var r = result.Result as CreatedResult;
            var dtoResult = (EventDto)r.Value;
            Assert.Equal("Jaha", dtoResult.EventName);

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
