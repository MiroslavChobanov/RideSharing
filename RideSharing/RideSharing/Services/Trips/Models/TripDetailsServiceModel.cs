namespace RideSharing.Services.Trips.Models
{
    public class TripDetailsServiceModel
    {
        public int Id { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime? EndTime { get; init; }
        public TimeSpan Duration { get; init; }
        public string PickUpLocation { get; init; }
        public string DropOffLocation { get; init; }
        public int Seats { get; init; }
        public decimal TripCost { get; init; }
    }
}
