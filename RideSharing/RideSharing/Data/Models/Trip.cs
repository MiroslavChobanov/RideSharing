namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Trip
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        [Required]

        public string PickUpLocation { get; set; }
        [Required]
        public string DropOffLocation { get; set; }
        public int Seats { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public decimal TripCost { get; set; }
        public double? TripRating { get; set; }
        
        public bool isPublic { get; set; }

        public IEnumerable<RiderTrip> RidersTrips { get; set; } = new List<RiderTrip>();
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();


    }
}
