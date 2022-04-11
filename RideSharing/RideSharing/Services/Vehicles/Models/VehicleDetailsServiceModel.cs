namespace RideSharing.Services.Vehicles.Models
{
    using RideSharing.Data.Models;
    public class VehicleDetailsServiceModel
    {
        public int Id { get; init; }
        public string Brand { get; init; }
        public string Model { get; init; }
        public int YearOfCreation { get; init; }
        public DateTime LastServicingDate { get; init; }
        public int VehicleTypeId { get; init; }
        public VehicleType VehicleType { get; init; }
        public string DriverId { get; init; }
        public string DriverName { get; init; }

        public string ImagePath { get; init; }

        public string UserId { get; init; }
        public bool IsDeleted { get; init; }

    }
}
