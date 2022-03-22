namespace RideSharing.Data.Models
{
    public class RiderTrip
    {
        public int RiderId { get; set; }
        public Rider Rider { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
