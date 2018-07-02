using System;
using System.Collections.Generic;
using System.Text;
using AcmeRemoteFlights.Services.Models;

namespace AcmeRemoteFlights.Services
{
    public class ValidationService : IValidationService
    {
        public ValidationResult ValidateAvailabilityParameters(DateTime fromDate, DateTime toDate, int numberOfPassengers)
        {
            var validationResult = new ValidationResult();
            
            if (fromDate > toDate)
            {
                validationResult.ValidationMessages.Add("From Date cannot be past To Date");                
            }

            if (numberOfPassengers <= 0)
            {
                validationResult.ValidationMessages.Add("From Date cannot be past To Date");                
            }
            
            return validationResult;
        }

    }
}
