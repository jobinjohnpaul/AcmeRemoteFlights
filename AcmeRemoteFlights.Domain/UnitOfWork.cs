using AcmeRemoteFlights.Domain.Models;
using AcmeRemoteFlights.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private FlightsDBContext _context;
        private IRepository<Flight> _flightRepository;
        private IRepository<BookedFlight> _bookedFlightsRepository;

        public UnitOfWork(IRepository<Flight> flightRepository, IRepository<BookedFlight> bookedFlightsRepository,
            FlightsDBContext context)
        {
            _flightRepository = flightRepository;
            _bookedFlightsRepository = bookedFlightsRepository;
            _context = context;
        }

        public IRepository<Flight> FlightsRepository => _flightRepository;
        
        public IRepository<BookedFlight> BookedFlightsRepository => _bookedFlightsRepository;
        

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

