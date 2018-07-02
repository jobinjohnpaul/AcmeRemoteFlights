using AcmeRemoteFlights.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Domain
{
    public class FlightsDBContext : DbContext
    {
        public FlightsDBContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<BookedFlight> BookedFlights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var flightGuid1 = Guid.NewGuid();
            var flightGuid2 = Guid.NewGuid();

            modelBuilder.Entity<Flight>().HasData(
             new Flight{ FlightID = flightGuid1, FlightName = "Helicopter1", SeatingCapacity = 5 },
             new Flight { FlightID = flightGuid2, FlightName = "Helicopter2", SeatingCapacity = 5 });

            modelBuilder.Entity<BookedFlight>().HasData(
            new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-06-30"), FlightID = flightGuid1, NoOfPassengers = 3 },
            new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-07-05"), FlightID = flightGuid2, NoOfPassengers = 4 }
            );
        }
    }
}
