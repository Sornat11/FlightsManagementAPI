namespace FlightsManagementAPI.Services.FlightService
{
    public interface IFlightService
    {
        List<Flight> GetAllFlights();
        Flight GetFlight(int id);
        List<Flight> AddFlight(Flight flight);
        List<Flight>? UpdateFlight(int id, Flight request);

        List<Flight>? DeleteFlight(int id);

    }
}
