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


    }
}