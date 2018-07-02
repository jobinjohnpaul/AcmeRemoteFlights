using System;
using System.ComponentModel.DataAnnotations;

namespace AcmeRemoteFlights.Domain.Models
{
    public class Flight : EntityBase
    {
        public Guid FlightID { get; set; }

        [MaxLength(10)]
        public string FlightName { get; set; }

        public int SeatingCapacity { get; set; }
    }
}
