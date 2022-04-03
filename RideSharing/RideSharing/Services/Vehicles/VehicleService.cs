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
            DateTime lastServicingDate, string imagePath, int vehicleTypeId, int driverId)
        {
            var vehicleData = new Vehicle
            {
                Brand = brand,
                Model = model,
                YearOfCreation = yearOfCreation,
                LastServicingDate = lastServicingDate,
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
            DateTime lastServicingDate,
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
            vehicleData.LastServicingDate = lastServicingDate;
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
                    Id = v.Id,
                    Brand = v.Brand,
                    Model = v.Model,
                    YearOfCreation = v.YearOfCreation,
                    VehicleTypeId = v.VehicleTypeId,
                    DriverId = v.DriverId.ToString(),
                    DriverName = v.Driver.FirstName + " " + v.Driver.LastName,
                    ImagePath = v.ImagePath
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

        public List<VehicleDetailsServiceModel> All()
        {
            return this.data
                 .Vehicles
                 .Select(v => new VehicleDetailsServiceModel
                 {
                     Id = v.Id,
                     Brand = v.Brand,
                     Model = v.Model,
                     YearOfCreation = v.YearOfCreation,
                     VehicleTypeId = v.VehicleTypeId,
                     VehicleType = v.VehicleType,
                     DriverId = v.DriverId.ToString(),
                     DriverName = v.Driver.FirstName + " " + v.Driver.LastName,
                     ImagePath = v.ImagePath
                 })
                 .OrderByDescending(v => v.YearOfCreation)
                 .ToList();
        }

        public VehicleDeleteServiceModel DeleteViewData(int id)
        {
            return this.data.Vehicles
                .Where(v => v.Id == id)
                .Select(v => new VehicleDeleteServiceModel
                {
                    Brand = v.Brand,
                    Model = v.Model,
                    YearOfCreation = v.YearOfCreation,
                    ImagePath = v.ImagePath
                })
                .First();
        }

        public bool Delete(int id)
        {
            var vehicleData = this.data.Vehicles.Find(id);

            if (vehicleData == null)
            {
                return false;
            }

            this.data.Vehicles.Remove(vehicleData);
            this.data.SaveChanges();

            return true;
        }

        public bool IsByDealer(int vehicleId, int driverId)
            => this.data
                .Vehicles
                .Any(v => v.Id == vehicleId && v.DriverId == driverId);
    }
}
