using AcmeRemoteFlights.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Domain.Repositories
{
    public class FlightRepository : RepositoryBase<Flight>
    {
        public FlightRepository(FlightsDBContext context):
            base(context)
        {

        }
    }
}
