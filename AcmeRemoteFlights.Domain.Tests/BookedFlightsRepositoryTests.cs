using AcmeRemoteFlights.Domain.Models;
using AcmeRemoteFlights.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace AcmeRemoteFlights.Domain.Tests
{
    [TestFixture]
    public class BookedFlightsRepositoryTests
    {
        [Test]
        public void BookedFlightsRepositoryTests_GetAllResults()
        {
            var options = new DbContextOptionsBuilder<FlightsDBContext>()
               .UseInMemoryDatabase(databaseName: "Find_searches_url")
               .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new FlightsDBContext(options))
            {
                var flightGuid1 = Guid.NewGuid();
                var flightGuid2 = Guid.NewGuid();
                context.Flights.Add(new Flight { FlightID = flightGuid1, FlightName = "Helicopter1", SeatingCapacity = 5 });
                context.Flights.Add(new Flight { FlightID = flightGuid2, FlightName = "Helicopter2", SeatingCapacity = 5 });

                context.BookedFlights.Add(new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-06-30"), FlightID = flightGuid1, NoOfPassengers = 3 });
                context.BookedFlights.Add(new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-07-05"), FlightID = flightGuid2, NoOfPassengers = 4 });
                
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new FlightsDBContext(options))
            {
                var bookedFlightsRepository = new BookedFlightsRepository(context);
                var result = bookedFlightsRepository.Get().ToList();
                Assert.AreEqual(2, result.Count());
            }
        }

        [Test]
        public void BookedFlightsRepositoryTests_GetFilteredResults()
        {
            //Arrange
            var fromDate = DateTime.Today;
            var toDate = DateTime.Today.AddDays(2);
            var numberOfPassengers = 2;


            var options = new DbContextOptionsBuilder<FlightsDBContext>()
               .UseInMemoryDatabase(databaseName: "Find_searches_url")
               .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new FlightsDBContext(options))
            {
                var flightGuid1 = Guid.NewGuid();
                var flightGuid2 = Guid.NewGuid();
                context.Flights.Add(new Flight { FlightID = flightGuid1, FlightName = "Helicopter1", SeatingCapacity = 5 });
                context.Flights.Add(new Flight { FlightID = flightGuid2, FlightName = "Helicopter2", SeatingCapacity = 5 });

                context.BookedFlights.Add(new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-06-30"), FlightID = flightGuid1, NoOfPassengers = 3 });
                context.BookedFlights.Add(new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-07-05"), FlightID = flightGuid2, NoOfPassengers = 4 });

                context.SaveChanges();
            }

            //Act & Assert
            // Use a clean instance of the context to run the test
            using (var context = new FlightsDBContext(options))
            {
                var bookedFlightsRepository = new BookedFlightsRepository(context);
                var result = bookedFlightsRepository.Get(
                    (bookedFlight) => bookedFlight.TravelDate >= fromDate
                                    && bookedFlight.TravelDate <= toDate
                                    && bookedFlight.NoOfPassengers < bookedFlight.Flight.SeatingCapacity
                                    && (bookedFlight.Flight.SeatingCapacity - bookedFlight.NoOfPassengers) >= numberOfPassengers

                    ).ToList();
                Assert.AreEqual(0, result.Count());
            }
        }
    }
}
