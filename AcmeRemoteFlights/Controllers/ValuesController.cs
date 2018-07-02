using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AcmeRemoteFlights.Services;
using AcmeRemoteFlights.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcmeRemoteFlights.Controllers
{
    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        private readonly IBookingEngineService _bookingEngineService;
        private readonly IValidationService _validationService;
        
        public FlightsController(IBookingEngineService bookingEngineService, IValidationService validationService)
        {
            _bookingEngineService = bookingEngineService;
            _validationService = validationService;
        }

        // GET api/CheckAvailability
        [HttpGet]
        public HttpResponseMessage CheckAvailability(DateTime fromDate, DateTime toDate, int numberOfPassengers)
        {
            var validationMessages = _validationService.ValidateAvailabilityParameters(fromDate, toDate, numberOfPassengers);

            if(validationMessages.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.OK) { ReasonPhrase = string.Join(" ", validationMessages.ValidationMessages)};
            }


            var availableFlightsDTO = _bookingEngineService.GetAvailableFlights(fromDate, toDate, numberOfPassengers);

            
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = (HttpContent) availableFlightsDTO };

        }        
    }
}
