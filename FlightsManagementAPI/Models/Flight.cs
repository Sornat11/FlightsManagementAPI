using System.ComponentModel.DataAnnotations;

namespace FlightsManagementAPI.Models
{
    public class Flight
    {
        public int Id { get; set; }

        public required string FlightNumber { get; set; }

        public DateTime DepartureDate { get; set; }

        [Required]
        public string DeparturePlace { get; set; } = string.Empty;
        [Required]
        public string ArrivalPlace { get; set; } = string.Empty;
        [Required]
        public string AircraftType { get; set; } = string.Empty;
    }
}
