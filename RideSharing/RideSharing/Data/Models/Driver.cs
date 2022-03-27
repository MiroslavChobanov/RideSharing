namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Driver;
    
    public class Driver
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }
        public DateTime StartedOn { get; set; }
        [Required]
        public string UserId { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; init; } = new List<Vehicle>();
        public IEnumerable<Trip> Trips { get; init; } = new List<Trip>();
        public IEnumerable<Payment> Payments { get; init; } = new List<Payment>();

    }
}
