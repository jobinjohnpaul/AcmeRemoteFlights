using System.Collections.Generic;

namespace AcmeRemoteFlights.Services.Models
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            ValidationMessages = new List<string>();
        }

        public bool IsValid  => ValidationMessages.Count > 0;

        public List<string> ValidationMessages { get; set; }
    }
}