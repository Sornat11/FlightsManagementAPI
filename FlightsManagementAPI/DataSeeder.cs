using FlightsManagementAPI.Models;
using FlightsManagementAPI.Data;


public class DataSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DataContext>();

            // Dodajemy loty tylko jeśli baza jest pusta
            if (!context.Flights.Any())
            {
                context.Flights.AddRange(
                    new Flight
                    {
                        FlightNumber = "AB123",
                        DepartureDate = DateTime.Now.AddDays(10),
                        DeparturePlace = "Warsaw",
                        ArrivalPlace = "London",
                        AircraftType = "Boeing 737"
                    },
            new Flight
            {
                FlightNumber = "CD456",
                DepartureDate = DateTime.Now.AddDays(20),
                DeparturePlace = "Krakow",
                ArrivalPlace = "Berlin",
                AircraftType = "Airbus A320"
            },
            new Flight
            {
                FlightNumber = "EF789",
                DepartureDate = DateTime.Now.AddDays(30),
                DeparturePlace = "Gdansk",
                ArrivalPlace = "Paris",
                AircraftType = "Boeing 747"
            },
            new Flight
            {
                FlightNumber = "GH012",
                DepartureDate = DateTime.Now.AddDays(15),
                DeparturePlace = "Wroclaw",
                ArrivalPlace = "Rome",
                AircraftType = "Airbus A380"
            },
            new Flight
            {
                FlightNumber = "IJ345",
                DepartureDate = DateTime.Now.AddDays(25),
                DeparturePlace = "Poznan",
                ArrivalPlace = "Madrid",
                AircraftType = "Boeing 777"
            });

            context.SaveChanges();
            }
        }
    }
}
