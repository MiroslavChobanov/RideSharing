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
            int VehicleTypeId,
            int driverId);

        public bool VehicleTypeExists(int vehicleTypeId);
    }
}
