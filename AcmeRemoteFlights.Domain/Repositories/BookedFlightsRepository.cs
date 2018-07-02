using AcmeRemoteFlights.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Domain.Repositories
{
    public class BookedFlightsRepository : RepositoryBase<BookedFlight>
    {
        public BookedFlightsRepository(FlightsDBContext context):
            base(context)
        {

        }
    }
}
