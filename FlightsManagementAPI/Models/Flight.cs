using System.ComponentModel.DataAnnotations;

namespace FlightsManagementAPI.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Flight number is required.")]
        [RegularExpression(@"^[A-Za-z]{2}\d{3}$", ErrorMessage = "Flight number format should be like 'AB123'.")]
        public string? FlightNumber { get; set; }

        [Required(ErrorMessage = "Departure date is required.")]
        [FutureDate(ErrorMessage = "Departure date must be in the future.")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Departure place is required.")]
        public string DeparturePlace { get; set; } = string.Empty;

        [Required(ErrorMessage = "Arrival place is required.")]
        public string ArrivalPlace { get; set; } = string.Empty;

        [Required(ErrorMessage = "Aircraft type is required.")]
        public string AircraftType { get; set; } = string.Empty;
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date && date < DateTime.Now)
            {
                return new ValidationResult(ErrorMessage ?? "The date must be in the future.", new[] { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }
    }
}
