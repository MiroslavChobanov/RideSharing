namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class Driver
    {
        
        public int Id { get; init; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        public int DriversLicenseId { get; set; }
        public DateTime StartedOn { get; set; }
        [Required]
        public string UserId { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; init; } = new List<Vehicle>();
        public IEnumerable<Trip> Trips { get; init; } = new List<Trip>();
        public IEnumerable<Payment> Payments { get; init; } = new List<Payment>();

    }
}
