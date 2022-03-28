namespace RideSharing.Services.Vehicles.Models
{
    public class VehicleDetailsServiceModel
    {
        public int Id { get; init; }
        public int YearOfCreation { get; init; }
        public int VehicleTypeId { get; init; }
        public int DriverId { get; init; }
        public string DriverName { get; init; }

        public string UserId { get; init; }

    }
}
