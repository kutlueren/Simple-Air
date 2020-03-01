using Microsoft.AspNetCore.Mvc;
using SimpleAir.API.Logging;
using SimpleAir.Domain.Service.Interface;
using SimpleAir.Domain.Service.Model.Airport;
using SimpleAir.Domain.Service.Model.Flight;
using System;
using System.Threading.Tasks;

namespace SimpleAir.API.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService ?? throw new ArgumentNullException(nameof(flightService));
        }

        [HttpPost]
        [Route("api/GetFlights")]
        [TrackUsage("Flight", "API", "GetFlights")]
        public async Task<IActionResult> GetFlights([FromBody] FlightRequestDto request)
        {
            var flights = await _flightService.GetAvailableFligthsAsync(request);

            return Ok(flights);
        }

        [HttpPost]
        [Route("api/GetAirports")]
        [TrackUsage("Flight", "API", "GetAirports")]
        public async Task<IActionResult> GetAirports([FromBody] AirportRequestDto request)
        {
            var airports = await _flightService.GetAirportsAsync(request);

            return Ok(airports);
        }
    }
}