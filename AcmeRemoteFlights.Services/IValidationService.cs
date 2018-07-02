using AcmeRemoteFlights.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeRemoteFlights.Services
{
    public interface IValidationService
    {
        ValidationResult ValidateAvailabilityParameters(DateTime fromDate, DateTime toDate, int numberOfPassengers);

    }
}
