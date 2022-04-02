namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Vehicle;

    public class Vehicle
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; }
        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }
        public int YearOfCreation { get; set; }
        public DateTime LastServicingDate { get; set; }
        public string ImagePath { get; set; }

        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; init; }
        public int DriverId { get; init; }

        public Driver Driver { get; init; }

        public int? RiderId { get; init; }

        public Rider Rider { get; init; }
        [Required]
        public bool IsDeleted { get; set; }

        public IEnumerable<Trip> Trips { get; init; } = new List<Trip>();



    }
}
