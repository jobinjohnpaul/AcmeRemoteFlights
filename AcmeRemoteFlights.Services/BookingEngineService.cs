using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcmeRemoteFlights.Domain;
using AcmeRemoteFlights.Domain.Models;
using AcmeRemoteFlights.Domain.Repositories;
using AcmeRemoteFlights.Services.Map;
using AcmeRemoteFlights.Services.Models;

namespace AcmeRemoteFlights.Services
{
    public class BookingEngineService : IBookingEngineService
    {
        private IUnitOfWork _unitOfWork;

        public BookingEngineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AvailableFlightsDTO> GetAvailableFlights(DateTime fromDate, DateTime toDate, int numberOfPassengers)
        {
            var modifiedFlightList = new List<BookedFlight>();

            //Get all booked flights for period
            var bookedFlights = _unitOfWork.BookedFlightsRepository.Get((bookedFlight) => bookedFlight.TravelDate >= fromDate
                                                                                         && bookedFlight.TravelDate <= toDate
                                                                       );
            //Get all configured flights
            var flights = _unitOfWork.FlightsRepository.Get().ToList();

            for (var startDate = fromDate; startDate <= toDate; startDate = startDate.AddDays(1))
            {
                foreach(var flight in flights)
                {
                    var bookedFlightItem = bookedFlights == null ? null :
                                            bookedFlights.ToList().FirstOrDefault(bookedFlight => bookedFlight.TravelDate == startDate 
                                                                                                && bookedFlight.FlightID == flight.FlightID);

                    if (bookedFlightItem == null)
                    {
                        modifiedFlightList.Add(new BookedFlight { BookedFlightID = Guid.NewGuid(), Flight = flight, FlightID = flight.FlightID, NoOfPassengers = 0 });
                    }
                    else if(bookedFlightItem.NoOfPassengers < bookedFlightItem.Flight.SeatingCapacity 
                        && (bookedFlightItem.Flight.SeatingCapacity - bookedFlightItem.NoOfPassengers) >= numberOfPassengers)
                    {
                        modifiedFlightList.Add(bookedFlightItem);
                    }
                }
            }

            return AvailableFlightsMap.CreateAvailableFlightsDTO(modifiedFlightList);
        }
    }
}
