using AcmeRemoteFlights.Domain.Models;
using AcmeRemoteFlights.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Domain
{
   public interface IUnitOfWork : IDisposable
    {
        IRepository<Flight> FlightsRepository { get; }

        IRepository<BookedFlight> BookedFlightsRepository { get; }

        void Save();
    }
}
