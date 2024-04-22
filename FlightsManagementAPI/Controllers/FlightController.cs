using FlightsManagementAPI.Models;
using FlightsManagementAPI.Services.FlightService;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Flight>>> GetAllFlights()
        {
            var flights = await _flightService.GetAllFlights();
            if (flights == null || flights.Count == 0)
            {
                return NotFound("No flights found.");
            }
            return Ok(flights);
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var result = await _flightService.GetFlight(id);
            if (result is null)
                    return NotFound("Flight not found.");

            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Flight>>> AddFlight(Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _flightService.AddFlight(flight);
            return Ok(result);
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Flight>>> UpdateFlight(int id, Flight request)
        {
            var result = await _flightService.UpdateFlight(id, request);

            if (result is null)
                return NotFound("Flight not found.");

            return Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Flight>>> DeleteFlight(int id)
        {
            var result = await _flightService.DeleteFlight(id);

            if (result is null)
                return NotFound("Flight not found.");

            return Ok(result);
        }
    }
}
