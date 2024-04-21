using FlightsManagementAPI.Models;
using FlightsManagementAPI.Services.FlightService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightsManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetAllFlights()
        {

            var result = _flightService.GetAllFlights();
            return Ok(result);  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var result = _flightService.GetFlight(id);
            if (result is null)
                    return NotFound("Flight not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Flight>>> AddFlight(Flight flight)
        {
            var result = _flightService.AddFlight(flight);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Flight>>> UpdateFlight(int id, Flight request)
        {
            var result = _flightService.UpdateFlight(id, request);

            if (result is null)
                return NotFound("Flight not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Flight>>> DeleteFlight(int id)
        {
            var result = _flightService.DeleteFlight(id);

            if (result is null)
                return NotFound("Flight not found.");

            return Ok(result);
        }

    }
}
