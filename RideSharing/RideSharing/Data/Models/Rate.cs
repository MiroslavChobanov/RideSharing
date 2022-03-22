namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Rate
    {
        public int Id { get; init; }
        [Required]
        public string VehicleSize { get; set; }
        public decimal baseRate { get; set; }
        public DateTime TimeOfDay { get; set; }

        public IEnumerable<RateTrip> RatesTrips { get; set; } = new List<RateTrip>();
    }
}
