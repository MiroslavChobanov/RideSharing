namespace RideSharing.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int RiderId { get; set; }
        public Rider Rider { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }  

    }
}
