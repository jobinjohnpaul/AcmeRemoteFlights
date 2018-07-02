using AcmeRemoteFlights.Domain;
using AcmeRemoteFlights.Domain.Models;
using AcmeRemoteFlights.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AcmeRemoteFlights.Services.Tests
{
    [TestFixture]
    public class BookingEngineServiceTests
    {
        Mock<IUnitOfWork> unitOfWork;

        Mock<IRepository<BookedFlight>> bookedFlightsRepositoryMoq;
        Mock<IRepository<Flight>> flightsRepositoryMoq;

        BookingEngineService bookingEngineService;
        Guid flightGuid1 = Guid.NewGuid();
        Guid flightGuid2 = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();

            bookedFlightsRepositoryMoq = new Mock<IRepository<BookedFlight>>();
            flightsRepositoryMoq = new Mock<IRepository<Flight>>();

            flightsRepositoryMoq.Setup(method => method.Get(
                         It.IsAny<Expression<Func<Flight, bool>>>(),
                         It.IsAny<Func<IQueryable<Flight>, IOrderedQueryable<Flight>>>(),
                         It.IsAny<string>()))
            .Returns(
                       new List<Flight>
                       {
                             new Flight{ FlightID = flightGuid1, FlightName = "Helicopter1", SeatingCapacity = 5 },
                             new Flight { FlightID = flightGuid2, FlightName = "Helicopter2", SeatingCapacity = 5 }
                       }
            );

            unitOfWork.SetupGet(method => method.FlightsRepository).Returns(flightsRepositoryMoq.Object);

            bookingEngineService = new BookingEngineService(unitOfWork.Object);
        }

        [Test]
        public void GetAvailableFlightsTest_ValidResponse()
        {
            //Arrange
            bookedFlightsRepositoryMoq.Setup(method => method.Get(
                         It.IsAny<Expression<Func<BookedFlight, bool>>>(),
                         It.IsAny<Func<IQueryable<BookedFlight>, IOrderedQueryable<BookedFlight>>>(),
                         It.IsAny<string>()))
            .Returns(
                       new List<BookedFlight>
                       {
                            new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-06-30"), NoOfPassengers = 3, Flight = new Flight{ FlightName = "Flight1", SeatingCapacity = 5, FlightID = flightGuid1}  },
                            new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-07-05"), NoOfPassengers = 4, Flight = new Flight{ FlightName = "Flight2", SeatingCapacity = 5, FlightID = flightGuid2}  }
                       }
            );

           unitOfWork.SetupGet(method => method.BookedFlightsRepository).Returns(bookedFlightsRepositoryMoq.Object);

            //Act
            var bookedFlights = bookingEngineService.GetAvailableFlights(DateTime.Today, DateTime.Today, 3);

            //Assert
            Assert.IsNotNull(bookedFlights);
            Assert.AreEqual(2, bookedFlights.Count());
        }

        [Test]
        public void GetAvailableFlightsTest_EmptyListResponse()
        {
            //Arrange
            bookedFlightsRepositoryMoq.Setup(method => method.Get(
                         It.IsAny<Expression<Func<BookedFlight, bool>>>(),
                         It.IsAny<Func<IQueryable<BookedFlight>, IOrderedQueryable<BookedFlight>>>(),
                         It.IsAny<string>()))
            .Returns(
                       new List<BookedFlight>
                       {
                       }
            );
            unitOfWork.SetupGet(method => method.BookedFlightsRepository).Returns(bookedFlightsRepositoryMoq.Object);

            //Act
            var bookedFlights = bookingEngineService.GetAvailableFlights(DateTime.Today, DateTime.Today, 3);

            //Assert
            Assert.IsNotNull(bookedFlights);
            Assert.AreEqual(2, bookedFlights.Count());
        }

        [Test]
        public void GetAvailableFlightsTest_NullResponse()
        {
            //Arrange
            bookedFlightsRepositoryMoq.Setup(method => method.Get(
                         It.IsAny<Expression<Func<BookedFlight, bool>>>(),
                         It.IsAny<Func<IQueryable<BookedFlight>, IOrderedQueryable<BookedFlight>>>(),
                         It.IsAny<string>()))
            .Returns(
                       (List<BookedFlight>)null 
            );
            unitOfWork.SetupGet(method => method.BookedFlightsRepository).Returns(bookedFlightsRepositoryMoq.Object);

            //Act
            var bookedFlights = bookingEngineService.GetAvailableFlights(DateTime.Today, DateTime.Today, 3);

            //Assert
            Assert.IsNotNull(bookedFlights);
            Assert.AreEqual(2, bookedFlights.Count());
        }

        [Test]
        public void GetAvailableFlightsTest_BookingSpreadOver3Days()
        {
            //Arrange
            bookedFlightsRepositoryMoq.Setup(method => method.Get(
                         It.IsAny<Expression<Func<BookedFlight, bool>>>(),
                         It.IsAny<Func<IQueryable<BookedFlight>, IOrderedQueryable<BookedFlight>>>(),
                         It.IsAny<string>()))
            .Returns(
                       new List<BookedFlight>
                       {
                            new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-07-02"), NoOfPassengers = 3, FlightID = flightGuid1, Flight = new Flight{ FlightName = "Flight1", SeatingCapacity = 5, FlightID = flightGuid1}  },
                            new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-07-05"), NoOfPassengers = 4, FlightID = flightGuid2, Flight = new Flight{ FlightName = "Flight2", SeatingCapacity = 5, FlightID = flightGuid2}  }
                       }
            );

            unitOfWork.SetupGet(method => method.BookedFlightsRepository).Returns(bookedFlightsRepositoryMoq.Object);

            //Act
            var bookedFlights = bookingEngineService.GetAvailableFlights(DateTime.Today, DateTime.Today.AddDays(3), 3);

            //Assert
            Assert.IsNotNull(bookedFlights);
            Assert.AreEqual(6, bookedFlights.Count());
        }
    }
}
