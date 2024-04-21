
using FlightsManagementAPI.Data;
using FlightsManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightsManagementAPI.Services.FlightService
{
    public class FlightService : IFlightService
    {
        private readonly DataContext _context;

        public FlightService(DataContext context)
        {
            _context = context;
        }

        public async  Task<List<Flight>> AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return await _context.Flights.ToListAsync();
        }

        public async Task<List<Flight>?> UpdateFlight(int id, Flight request)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight is null)
                return null;

            flight.FlightNumber = request.FlightNumber;
            flight.DepartureDate = request.DepartureDate;
            flight.ArrivalPlace = request.ArrivalPlace;
            flight.DeparturePlace = request.DeparturePlace;
            flight.AircraftType = request.AircraftType;

            await _context.SaveChangesAsync();  

            return await _context.Flights.ToListAsync();
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            var flights = await _context.Flights.ToListAsync();
            return flights;
        }

        public async Task<Flight?> GetFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight is null)
                return null;
            return flight;
        }

        public async Task<List<Flight>?> DeleteFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight is null)
                return null;

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return await _context.Flights.ToListAsync();
        }
    }
    }

