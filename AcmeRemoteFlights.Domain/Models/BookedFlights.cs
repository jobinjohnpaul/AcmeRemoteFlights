using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Domain.Models
{
    public class BookedFlight : EntityBase
    {
        public Guid BookedFlightID { get; set; }
        public Guid FlightID { get; set; }
        public int NoOfPassengers { get; set; }
        public Flight Flight { get; set; }        
        public DateTime TravelDate { get; set; }

    }
}
