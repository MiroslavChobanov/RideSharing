namespace RideSharing.Test
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using RideSharing.Services.Drivers;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    public class DriverServiceTests
    {
        [Fact]
        public void JoinAsDriverWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("JoinAsDriverWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Drivers.Add(new Driver
            {
                FirstName = "Miroslav",
                LastName = "Chobanov",
                Gender = "Male",
                PhoneNumber = "0899123456",
                UserId = "1"
            });

            dbContext.SaveChanges();

            Assert.Single(dbContext.Drivers);
        }

        [Fact]
        public void IsDriverWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("IsDriverWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Drivers.Add(new Driver
            {
                FirstName = "Miroslav",
                LastName = "Chobanov",
                Gender = "Male",
                PhoneNumber = "0899123456",
                UserId = "1"
            });

            dbContext.Users.Add(new User
            {
                Id = "1",
                FirstName = "Miroslav",
                LastName = "Chobanov"
            });

            dbContext.SaveChanges();
            var service = new DriverService(dbContext);

            var isDriver = service.IsDriver("1");
            Assert.True(isDriver);
        }

        [Fact]
        public void IdByUserWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("IdByUserWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Drivers.Add(new Driver
            {
                FirstName = "Miroslav",
                LastName = "Chobanov",
                Gender = "Male",
                PhoneNumber = "0899123456",
                UserId = "1"
            });

            dbContext.Users.Add(new User
            {
                Id = "1",
                FirstName = "Miroslav",
                LastName = "Chobanov"
            });

            dbContext.SaveChanges();
            var service = new DriverService(dbContext);

            var idByUser = service.IdByUser("1");
            Assert.Equal(1, idByUser);
        }

        [Fact]
        public void DriverDetailsWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("DriverDetailsWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Drivers.Add(new Driver
            {
                FirstName = "Miroslav",
                LastName = "Chobanov",
                Gender = "Male",
                PhoneNumber = "0899123456",
                UserId = "1"
            });

            dbContext.SaveChanges();
            var service = new DriverService(dbContext);

            var driver = service.Details(1);
            Assert.Equal("Miroslav", driver.FirstName);
        }

        [Fact]
        public void EditDriverInformationWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("EditDriverInformationWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Drivers.Add(new Driver
            {
                FirstName = "Miroslav",
                LastName = "Chobanov",
                Gender = "Male",
                PhoneNumber = "0899123456",
                UserId = "1"
            });

            dbContext.SaveChanges();
            var service = new DriverService(dbContext);

            service.Edit(
            1,
            "Ivan",
            "Ivanov",
            "Male",
            "0878834568"
            );

            var driver = dbContext.Drivers.Find(1);
            Assert.Equal("Ivan", driver.FirstName);
        }

        [Fact]
        public void ResignAsDriverWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("ResignAsDriverWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Drivers.Add(new Driver
            {
                FirstName = "Miroslav",
                LastName = "Chobanov",
                Gender = "Male",
                PhoneNumber = "0899123456",
                UserId = "1"
            });
            dbContext.Drivers.Add(new Driver
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Gender = "Male",
                PhoneNumber = "0899236789",
                UserId = "2"
            });

            dbContext.SaveChanges();
            var service = new DriverService(dbContext);

            service.Resign(1);
            Assert.Single(dbContext.Drivers);
        }
    }
}
