namespace RideSharing.Services.Vehicles
{
    public interface IVehicleService
    {
        int Create(
            string brand,
            string model,
            int yearOfCreation,
            string lastServicingDate,
            string ImagePath,
            int VehicleTypeId);
    }
}
