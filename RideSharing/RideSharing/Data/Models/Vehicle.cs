namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vehicle
    {
        public int Id { get; init; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Make { get; set; }
        public int YearOfCreation { get; set; }
        public DateTime LastServicingDate { get; set; }
        public string ImagePath { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }

        public IEnumerable<Trip> Trips { get; init; } = new List<Trip>();

    }
}
