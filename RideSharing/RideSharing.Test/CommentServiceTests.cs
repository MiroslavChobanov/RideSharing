namespace RideSharing.Test
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using RideSharing.Services.Comments;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CommentServiceTests
    {
        [Fact]
        public void AddCommentToTrip()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("AddCommentToTrip").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Description = "desc",
                Date = System.DateTime.Today,
                RiderId = 1,
                TripId = 1
            });

            dbContext.SaveChanges();
            var service = new CommentService(dbContext);

            Assert.Single(dbContext.Comments);
        }

        [Fact]
        public void EditCommentWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("EditCommentWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Description = "desc",
                Date = System.DateTime.Today,
                RiderId = 1,
                TripId = 1
            });

            dbContext.SaveChanges();
            var service = new CommentService(dbContext);

            service.Edit(
            1,
            "new desc",
            System.DateTime.Today
            );

            var comment = dbContext.Comments.Find(1);
            Assert.Equal("new desc", comment.Description);
        }

        [Fact]
        public void IsByRiderWorks()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("IsByRiderWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Description = "desc",
                Date = System.DateTime.Today,
                RiderId = 1,
                TripId = 1
            });

            dbContext.Riders.Add(new Rider
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Gender = "Male",
                PhoneNumber = "0888812345",
                UserId = "1"
            });

            dbContext.SaveChanges();
            var service = new CommentService(dbContext);

            var isByRider = service.IsByRider(1, 1);
            Assert.True(isByRider);
        }

        [Fact]
        public void ReturnsCorrectIdOfTrip()
        {
            var options = new DbContextOptionsBuilder<RideSharingDbContext>().UseInMemoryDatabase("IsByRiderWorks").Options;
            var dbContext = new RideSharingDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Description = "desc",
                Date = System.DateTime.Today,
                RiderId = 1,
                TripId = 1
            });

            dbContext.Trips.Add(new Trip
            {
                StartTime = System.DateTime.Today,
                Duration = System.TimeSpan.MinValue,
                PickUpLocation = "Sofia",
                DropOffLocation = "Varna",
                Seats = 3,
                TripCost = 13.40M,
                DriverId = 1,
                VehicleId = 1
            });

            dbContext.SaveChanges();
            var service = new CommentService(dbContext);

            var idOfTrip = service.IdOfTrip(1);
            Assert.Equal(1,idOfTrip);
        }
    }
}
