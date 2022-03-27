using RideSharing.Services.Vehicles.Models;

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

        bool IsByDriver(int vehicleId, int driverId);

        bool Edit(
            int vehicleId,
            string brand,
            string model,
            int yearOfCreation,
            string lastServicingDate,
            string imagePath,
            int vehicleTypeId);

        VehicleDetailsServiceModel Details(int vehicleId);

        IEnumerable<VehicleVehicleTypeServiceModel> AllVehicleTypes();
    }
}
