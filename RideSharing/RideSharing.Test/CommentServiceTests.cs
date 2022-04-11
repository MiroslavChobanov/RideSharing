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
    }
}
