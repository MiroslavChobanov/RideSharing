namespace RideSharing.Services.Vehicles
{
    using Microsoft.EntityFrameworkCore;
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using RideSharing.Services.Vehicles.Models;
    using System.Collections.Generic;

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
        public bool Edit(
            int vehicleId,
            string brand,
            string model,
            int yearOfCreation,
            string lastServicingDate,
            string imagePath,
            int vehicleTypeId)
        {
            var vehicleData = this.data.Vehicles.Find(vehicleId);

            if (vehicleData == null)
            {
                return false;
            }

            vehicleData.Brand = brand;
            vehicleData.Model = model;
            vehicleData.YearOfCreation = yearOfCreation;
            vehicleData.LastServicingDate = DateTime.Parse(lastServicingDate);
            vehicleData.ImagePath = imagePath;
            vehicleData.VehicleTypeId = vehicleTypeId;

            this.data.SaveChanges();

            return true;
        }

        public bool IsByDriver(int vehicleId, int driverId)
        {
            return this.data
                .Vehicles
                .Any(v => v.Id == vehicleId && v.DriverId == driverId);
        }

        public VehicleDetailsServiceModel Details(int id)
        {
            return this.data.Vehicles
                .Where(v => v.Id == id)
                .Select(v => new VehicleDetailsServiceModel
                {
                    YearOfCreation = v.YearOfCreation,
                    VehicleTypeId = v.VehicleTypeId,
                    DriverId = v.DriverId,
                    DriverName = v.Driver.FirstName + " " + v.Driver.LastName
                })
                .First();
        }

        public IEnumerable<VehicleVehicleTypeServiceModel> AllVehicleTypes()
        {
            return this.data
                .VehicleTypes
                .Select(vt => new VehicleVehicleTypeServiceModel
                {
                    Id = vt.Id,
                    Type = vt.Type
                })
                .ToList();
        }
    }
}
