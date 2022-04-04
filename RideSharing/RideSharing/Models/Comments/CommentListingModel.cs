namespace RideSharing.Models.Comments
{
    using RideSharing.Data.Models;
    public class CommentListingModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public DateTime? LastEditedOn { get; set; }

        public int TripId { get; set; }

        public int RiderId { get; set; }

        public Rider Rider { get; set; }

    }
}
