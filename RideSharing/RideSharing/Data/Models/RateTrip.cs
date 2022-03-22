namespace RideSharing.Data.Models
{
    public class RateTrip
    {
        public int RateId { get; set; }
        public Rate Rate { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
