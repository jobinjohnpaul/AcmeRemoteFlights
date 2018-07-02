using AcmeRemoteFlights.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Domain
{
    public static class DbInitializer
    {
        public static void Initialize(FlightsDBContext context)
        {
            if(context.Database.EnsureCreated())
            {
                //Seed the database
                var flightGuid1 = Guid.NewGuid();
                var flightGuid2 = Guid.NewGuid();

                context.Flights.Add(new Flight { FlightID = flightGuid1, FlightName = "Helicopter1", SeatingCapacity = 5 });
                context.Flights.Add(new Flight { FlightID = flightGuid2, FlightName = "Helicopter2", SeatingCapacity = 5 });

                context.SaveChanges();

                context.BookedFlights.Add(new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-06-30"), FlightID = flightGuid1, NoOfPassengers = 3 });
                context.BookedFlights.Add(new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-07-05"), FlightID = flightGuid2, NoOfPassengers = 4 });

                context.BookedFlights.Add(new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-07-07"), FlightID = flightGuid2, NoOfPassengers = 4 });
                context.BookedFlights.Add(new BookedFlight { BookedFlightID = Guid.NewGuid(), TravelDate = DateTime.Parse("2018-07-07"), FlightID = flightGuid1, NoOfPassengers = 4 });


                context.SaveChanges();
            }
        }

    }
}
