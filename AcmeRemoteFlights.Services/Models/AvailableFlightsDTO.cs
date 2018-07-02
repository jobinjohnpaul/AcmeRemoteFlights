﻿using AcmeRemoteFlights.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Services.Models
{
    public class AvailableFlightsDTO
    {
        public int AvailableSeats { get; set; }
        public Flight AvailableFlight { get; set; }
    }
}