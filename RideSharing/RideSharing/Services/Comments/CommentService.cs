namespace RideSharing.Services.Comments
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using RideSharing.Models.Comments;

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

        public bool Edit(int id, string description, DateTime lastEditedOn)
        {
            var commentData = this.data.Comments.Find(id);

            if (commentData == null)
            {
                return false;
            }

            commentData.Description = description;
            commentData.LastEditedOn = lastEditedOn;

            this.data.SaveChanges();

            return true;
        }

        public bool IsByRider(int commentId, int riderId)
        {
            return this.data
                .Comments
                .Any(c => c.Id == commentId && c.RiderId == riderId);
        }

        public int IdOfTrip(int commentId)
        {
            return this.data
                .Comments
                .Where(c => c.Id == commentId)
                .Select(c => c.TripId)
                .FirstOrDefault();
        }

        public bool Delete(int id)
        {
            var comment = this.data.Comments.Find(id);

            if (comment == null)
            {
                return false;
            }

            this.data.Remove(comment);

            this.data.SaveChanges();

            return true;
        }

        public EditCommentFormModel EditViewData(int id)
        {
            return this.data.Comments
                    .Where(c => c.Id == id)
                    .Select(c => new EditCommentFormModel
                    {
                        Description = c.Description
                    })
                    .First();
        }
    }
}
