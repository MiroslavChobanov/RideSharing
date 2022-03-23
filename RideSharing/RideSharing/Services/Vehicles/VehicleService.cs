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
        public int Create(string model, string make, int yearOfCreation,
            string lastServicingDate, string imagePath, int vehicleTypeId)
        {
            var vehicleData = new Vehicle
            {
                Model = model,
                Make = make,
                YearOfCreation = yearOfCreation,
                LastServicingDate = DateTime.Parse(lastServicingDate),
                ImagePath = imagePath,
                VehicleTypeId = vehicleTypeId
            };

            this.data.Vehicles.Add(vehicleData);
            this.data.SaveChanges();

            return vehicleData.Id;
        }
    }
}
