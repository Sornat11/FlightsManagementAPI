
namespace FlightsManagementAPI.Services.FlightService
{
    public class FlightService : IFlightService
    {
        private static List<Flight> flights = new List<Flight>
            {
                new Flight { Id = 1, FlightNumber = "AA123", DepartureDate = DateTime.Now.AddDays(1), DeparturePlace = "New York", ArrivalPlace = "Los Angeles", AircraftType = "Boeing 737" },
                new Flight { Id = 2, FlightNumber = "BA456", DepartureDate = DateTime.Now.AddDays(2), DeparturePlace = "London", ArrivalPlace = "Paris", AircraftType = "Airbus A320" },
                new Flight { Id = 3, FlightNumber = "DL789", DepartureDate = DateTime.Now.AddDays(3), DeparturePlace = "Tokyo", ArrivalPlace = "Dubai", AircraftType = "Boeing 777" },
                new Flight { Id = 4, FlightNumber = "EK101", DepartureDate = DateTime.Now.AddDays(4), DeparturePlace = "Sydney", ArrivalPlace = "Singapore", AircraftType = "Airbus A380" },
                new Flight { Id = 5, FlightNumber = "LH202", DepartureDate = DateTime.Now.AddDays(5), DeparturePlace = "Beijing", ArrivalPlace = "Moscow", AircraftType = "Boeing 747" }
            };

        public List<Flight> AddFlight(Flight flight)
        {
            flights.Add(flight);
            return flights;
        }

        public List<Flight>? UpdateFlight(int id, Flight request)
        {
            var flight = flights.Find(x => x.Id == id);
            if (flight is null)
                return null;

            flight.FlightNumber = request.FlightNumber;
            flight.DepartureDate = request.DepartureDate;
            flight.ArrivalPlace = request.ArrivalPlace;
            flight.DeparturePlace = request.DeparturePlace;
            flight.AircraftType = request.AircraftType;

            return flights;
        }

        public List<Flight> GetAllFlights()
        {
            return flights;
        }

        public Flight GetFlight(int id)
        {
            var flight = flights.Find(x => x.Id == id);
            if (flight is null)
                return null;
            return flight;
        }

        public List<Flight>? DeleteFlight(int id)
        {
            var flight = flights.Find(x => x.Id == id);
            if (flight is null)
                return null;

            flights.Remove(flight);

            return flights;
        }
    }
    }

