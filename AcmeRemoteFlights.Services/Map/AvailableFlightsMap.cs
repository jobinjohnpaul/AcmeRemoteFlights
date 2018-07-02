using AcmeRemoteFlights.Domain.Models;
using AcmeRemoteFlights.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Services.Map
{
    public static class AvailableFlightsMap
    {
        public static IEnumerable<AvailableFlightsDTO> CreateAvailableFlightsDTO(IEnumerable<BookedFlight> bookedFlights)
        {
            var availableFlights = new List<AvailableFlightsDTO>();

            if (bookedFlights == null)
                return availableFlights;

            foreach (var bookedFlight in bookedFlights)
            {
                var availableFlight = new AvailableFlightsDTO();

                availableFlight.AvailableFlight = bookedFlight.Flight;
                availableFlight.AvailableSeats = bookedFlight.Flight.SeatingCapacity - bookedFlight.NoOfPassengers;

                availableFlights.Add(availableFlight);
            }

            return availableFlights;
        }
    }
}
