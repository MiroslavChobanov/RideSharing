namespace RideSharing.Services.Vehicles.Models
{
    public class VehicleServiceModel
    {
        public int Id { get; init; }
        public string Brand { get; init; }
        public string Model { get; init; }
        public int YearOfCreation { get; init; }
        public string LastServicingDate { get; init; }
        public string ImagePath { get; init; }
        public string VehicleType { get; init; }
    }
}
