namespace RideSharing.Services.Trips.Models
{
    public class TripPostponeServiceModel
    {
        public DateTime StartTime { get; init; }
        public string PickUpLocation { get; init; }
        public string DropOffLocation { get; init; }

    }
}
