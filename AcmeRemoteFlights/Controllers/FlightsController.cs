using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AcmeRemoteFlights.Services;
using AcmeRemoteFlights.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public JsonResult CheckAvailability(DateTime fromDate, DateTime toDate, int numberOfPassengers)
        {
            var validationMessages = _validationService.ValidateAvailabilityParameters(fromDate, toDate, numberOfPassengers);
            JsonSerializer jsonSerializer = new JsonSerializer();
            
            if (validationMessages.IsValid)
            {
                //return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(validationMessages.ValidationMessages), Encoding.UTF8, "application/json")};
                return new JsonResult(validationMessages.ValidationMessages);
            }


            var availableFlightsDTO = _bookingEngineService.GetAvailableFlights(fromDate, toDate, numberOfPassengers);

            if (availableFlightsDTO.Count() == 0)
            { return new JsonResult("No seats available for provided dates and passenger number. Please modify your search and try again."); }

            //return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(availableFlightsDTO), Encoding.UTF8, "application/json") };
            return new JsonResult(availableFlightsDTO);

        }
    }
}
