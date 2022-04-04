namespace RideSharing.Services.Comments
{
    public interface ICommentService
    {
        public int AddCommentToTrip(string description, DateTime date, int riderId, int tripId);
    }
}
