namespace RideSharing.Services.Trips
{
    using RideSharing.Models.Trips;
    public interface ITripService
    {
        public List<TripListingModel> All();
         public int Add(DateTime startTime, DateTime? endTime, TimeSpan duration, string pickUpLocation,
            string dropOffLocation, int seats, decimal tripCost);
    }
}
