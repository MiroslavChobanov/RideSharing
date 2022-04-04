namespace RideSharing.Services.Comments
{
    using RideSharing.Models.Comments;
    public interface ICommentService
    {
        public int AddCommentToTrip(string description, DateTime date, int riderId, int tripId);
        public bool Edit(int id, string description, DateTime lastEditedOn);
        public bool IsByRider(int commentId, int riderId);
        public int IdOfTrip(int commentId);
        public bool Delete(int id);
        public EditCommentFormModel EditViewData(int id);



    }
}
