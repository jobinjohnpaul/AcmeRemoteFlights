using AcmeRemoteFlights.Domain.Models;
using AcmeRemoteFlights.Services.Models;
using System;
using System.Collections.Generic;

namespace AcmeRemoteFlights.Services
{
    public interface IBookingEngineService
    {
        IEnumerable<AvailableFlightsDTO> GetAvailableFlights(DateTime fromDate, DateTime toDate, int numberOfPeople);
    }
}
