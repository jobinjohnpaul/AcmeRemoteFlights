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
        
    }
}
