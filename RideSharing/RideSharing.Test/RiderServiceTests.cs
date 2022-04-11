namespace RideSharing.Test
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using RideSharing.Services.Riders;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    public class RiderServiceTests
    {
        [Fact]
        public void JoinAsRiderWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("JoinAsRiderWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Riders.Add(new Rider
            {
                FirstName = "Miroslav",
                LastName = "Chobanov",
                Gender = "Male",
                PhoneNumber = "0899123456",
                UserId = "1"
            });

            dbContext.SaveChanges();

            Assert.Single(dbContext.Riders);
        }

        [Fact]
        public void IsRiderWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("IsRiderWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Riders.Add(new Rider
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
            var service = new RiderService(dbContext);

            var isRider = service.IsRider("1");
            Assert.True(isRider);
        }

        [Fact]
        public void IdByUserWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("IdByUserWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Riders.Add(new Rider
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
            var service = new RiderService(dbContext);

            var idByUser = service.IdByUser("1");
            Assert.Equal(1, idByUser);
        }

        [Fact]
        public void EditRiderInformationWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("EditRiderInformationWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Riders.Add(new Rider
            {
                FirstName = "Miroslav",
                LastName = "Chobanov",
                Gender = "Male",
                PhoneNumber = "0899123456",
                UserId = "1"
            });

            dbContext.SaveChanges();
            var service = new RiderService(dbContext);

            service.Edit(
            1,
            "Ivan",
            "Ivanov",
            "Male",
            "0878834568"
            );

            var rider = dbContext.Riders.Find(1);
            Assert.Equal("Ivan", rider.FirstName);
        }

    }
}
