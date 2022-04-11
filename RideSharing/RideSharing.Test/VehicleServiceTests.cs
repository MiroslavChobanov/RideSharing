namespace RideSharing.Test
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    using RideSharing.Services.Vehicles;

    public class VehicleServiceTests
    {
        [Fact]
        public void AddVehicleWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("AddVehicleWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Vehicles.Add(new Vehicle
            {
                Brand = "Brand 1",
                Model = "Model 1",
                YearOfCreation = 1999,
                LastServicingDate = System.DateTime.Now,
                ImagePath = "",
                VehicleTypeId = 1,
                DriverId = 1
            });

            dbContext.SaveChanges();
            var service = new VehicleService(dbContext);

            Assert.Single(dbContext.Vehicles);
        }

        [Fact]
        public void EditVehicleWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("EditVehicleWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Vehicles.Add(new Vehicle
            {
                Brand = "Brand 1",
                Model = "Model 1",
                YearOfCreation = 1999,
                LastServicingDate = System.DateTime.Now,
                ImagePath = "",
                VehicleTypeId = 1,
                DriverId = 1
            });

            dbContext.SaveChanges();
            var service = new VehicleService(dbContext);

            service.Edit(
            1,
            "Brand 2",
            "Brand 3",
            2021,
            System.DateTime.Today,
            "",
            1);
            var vehicle = dbContext.Vehicles.Find(1);
            Assert.Equal("Brand 2", vehicle.Brand);
        }

        [Fact]
        public void IsByDriverWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("IsByDriverWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Vehicles.Add(new Vehicle
            {
                Brand = "Brand 1",
                Model = "Model 1",
                YearOfCreation = 1999,
                LastServicingDate = System.DateTime.Now,
                ImagePath = "",
                VehicleTypeId = 1,
                DriverId = 1
            });

            dbContext.Drivers.Add(new Driver
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Gender = "Male",
                PhoneNumber = "0888812345",
                UserId = "1"
            });

            dbContext.SaveChanges();
            var service = new VehicleService(dbContext);

            var isByDriver = service.IsByDriver(1, 1);
            Assert.True(isByDriver);
        }

        [Fact]
        public void AllVehicleTypesWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("AllVehicleTypesWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.VehicleTypes.Add(new VehicleType
            {
                Id = 1,
                Type = "Sedan"
            });

            dbContext.SaveChanges();
            var service = new VehicleService(dbContext);

            var vehicleTypes = service.AllVehicleTypes();
            Assert.Single(vehicleTypes);
        }
        [Fact]
        public void DeleteVehicleWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("DeleteVehicleWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Vehicles.Add(new Vehicle
            {
                Brand = "Brand 1",
                Model = "Model 1",
                YearOfCreation = 1999,
                ImagePath = "",
                VehicleTypeId = 1,
                DriverId = 1
            });
            dbContext.Vehicles.Add(new Vehicle
            {
                Brand = "Brand 2",
                Model = "Model 2",
                YearOfCreation = 2001,
                ImagePath = "",
                VehicleTypeId = 2,
                DriverId = 2
            });

            dbContext.SaveChanges();
            var service = new VehicleService(dbContext);

            service.Delete(1);
            Assert.Single(dbContext.Vehicles);
        }
    }
}