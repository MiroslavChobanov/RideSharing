namespace RideSharing.Test
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    using RideSharing.Services.Trips;
    using System.Collections.Generic;

    public class TripServiceTests
    {
        [Fact]
        public void AddTripWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("AddTripWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Trips.Add(new Trip
            {
                StartTime = System.DateTime.Today,
                Duration = System.TimeSpan.MinValue,
                PickUpLocation = "Sofia",
                DropOffLocation = "Plovdiv",
                Seats = 3,
                TripCost = 13.33M,
                DriverId = 1,
                VehicleId = 1
            });

            dbContext.SaveChanges();

            Assert.Single(dbContext.Trips);
        }

        [Fact]
        public void AllVehicles()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("AllVehicleTypesWorks").Options;
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
            var service = new TripService(dbContext);

            var vehicleTypes = service.AllVehicles();
            Assert.Single(vehicleTypes);
        }

        [Fact]
        public void EditTripWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("EditTripWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Trips.Add(new Trip
            {
                StartTime = System.DateTime.Today,
                EndTime = System.DateTime.MaxValue,
                Duration = System.TimeSpan.MinValue,
                PickUpLocation = "Sofia",
                DropOffLocation = "Plovdiv",
                Seats = 3,
                TripCost = 13.33M,
                DriverId = 1,
                VehicleId = 1
            });

            dbContext.SaveChanges();
            var service = new TripService(dbContext);

            service.Edit(
            1,
            System.DateTime.Today,
            System.DateTime.MaxValue,
            System.TimeSpan.MinValue,
            "Haskovo",
            "Varna",
            3,
            15.43M
            );

            var trip = dbContext.Trips.Find(1);
            Assert.Equal("Haskovo", trip.PickUpLocation);
        }

        [Fact]
        public void PostponeTripWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("PostponeTripWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Trips.Add(new Trip
            {
                StartTime = System.DateTime.Today,
                EndTime = System.DateTime.MaxValue,
                Duration = System.TimeSpan.MinValue,
                PickUpLocation = "Sofia",
                DropOffLocation = "Plovdiv",
                Seats = 3,
                TripCost = 13.33M,
                DriverId = 1,
                VehicleId = 1
            });
            dbContext.Trips.Add(new Trip
            {
                StartTime = System.DateTime.Today,
                EndTime = System.DateTime.MaxValue,
                Duration = System.TimeSpan.MinValue,
                PickUpLocation = "Varna",
                DropOffLocation = "Pleven",
                Seats = 2,
                TripCost = 13.33M,
                DriverId = 2,
                VehicleId = 2
            });

            dbContext.SaveChanges();
            var service = new TripService(dbContext);

            service.Postpone(1);
            Assert.Single(dbContext.Trips);
        }

        [Fact]
        public void AllTripsWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("AllTripsWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Trips.Add(new Trip
            {
                StartTime = System.DateTime.Today,
                EndTime = System.DateTime.MaxValue,
                Duration = System.TimeSpan.MinValue,
                PickUpLocation = "Sofia",
                DropOffLocation = "Plovdiv",
                Seats = 3,
                TripCost = 13.33M,
                DriverId = 1,
                VehicleId = 1
            });
            dbContext.Trips.Add(new Trip
            {
                StartTime = System.DateTime.Today,
                EndTime = System.DateTime.MaxValue,
                Duration = System.TimeSpan.MinValue,
                PickUpLocation = "Varna",
                DropOffLocation = "Pleven",
                Seats = 2,
                TripCost = 13.33M,
                DriverId = 2,
                VehicleId = 2
            });
            dbContext.SaveChanges();
            var service = new TripService(dbContext);

            Assert.Equal(2, service.All().Count);
        }
    }
}
