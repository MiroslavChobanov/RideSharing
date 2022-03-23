namespace RideSharing.Services.Vehicles
{
    public interface IVehicleService
    {
        int Create(
            string model,
            string make,
            int yearOfCreation,
            string lastServicingDate,
            string ImagePath,
            int VehicleTypeId);
    }
}
