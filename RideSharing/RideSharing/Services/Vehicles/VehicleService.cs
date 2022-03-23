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
        public int Create(string model, string make, string yearOfCreation, string lastServicingDate, string imagePath)
        {
            var vehicleData = new Vehicle
            {
                Model = model,
                Make = make,
                YearOfCreation = DateTime.Parse(yearOfCreation),
                LastServicingDate = DateTime.Parse(lastServicingDate),
                ImagePath = imagePath
            };

            this.data.Vehicles.Add(vehicleData);
            this.data.SaveChanges();

            return vehicleData.Id;
        }
    }
}
