using RideSharing.Data;
using RideSharing.Data.Models;

namespace RideSharing.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly RideSharingDbContext data;

        public CommentService(RideSharingDbContext data)
        {
            this.data = data;
        }
        public int AddCommentToTrip(string description, DateTime date, int riderId, int tripId)
        {
            var newComment = new Comment
            {
                Description = description,
                Date = date,
                RiderId = riderId,
                TripId = tripId
            };

            this.data.Comments.Add(newComment);
            this.data.SaveChanges();

            return newComment.Id;
        }
    }
}
