namespace RideSharing.Services.Vehicles
{
    using RideSharing.Data;
    using RideSharing.Data.Models;

    public class VehicleService : IVehicleService
    {
        private readonly RideSharingDbContext data;

        public VehicleService(RideSharingDbContext data)
        {
            this.data = data;
        }
        public int Create(string brand, string model, int yearOfCreation,
            string lastServicingDate, string imagePath, int vehicleTypeId, int driverId)
        {
            var vehicleData = new Vehicle
            {
                Brand = brand,
                Model = model,
                YearOfCreation = yearOfCreation,
                LastServicingDate = DateTime.Parse(lastServicingDate),
                ImagePath = imagePath,
                VehicleTypeId = vehicleTypeId,
                DriverId = driverId
            };

            this.data.Vehicles.Add(vehicleData);
            this.data.SaveChanges();

            return vehicleData.Id;
        }

        public bool VehicleTypeExists(int vehicleTypeId)
        {
            return this.data
                .VehicleTypes
                .Any(vt => vt.Id == vehicleTypeId);
        }
    }
}
