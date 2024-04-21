using FlightsManagementAPI.Models;

namespace FlightsManagementAPI.Services.FlightService
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAllFlights();
        Task<Flight?> GetFlight(int id);
        Task<List<Flight>> AddFlight(Flight flight);
        Task<List<Flight>?> UpdateFlight(int id, Flight request);

        Task<List<Flight>?> DeleteFlight(int id);

    }
}
