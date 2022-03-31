namespace RideSharing.Services.Trips
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using RideSharing.Models.Trips;

    public class TripService : ITripService
    {
        private readonly RideSharingDbContext data;

        public TripService(RideSharingDbContext data)
        {
            this.data = data;
        }
        public List<TripListingModel> All()
        {
            return this.data
                 .Trips
                 .Select(t => new TripListingModel
                 {
                     StartTime = t.StartTime,
                     EndTime = t.EndTime,
                     Duration = t.Duration,
                     PickUpLocation = t.PickUpLocation,
                     DropOffLocation = t.DropOffLocation,
                     Seats = t.Seats,
                     TripCost = t.TripCost
                 })
                 .OrderByDescending(x => x.StartTime)
                 .ToList();
        }

        public int Add(DateTime startTime, DateTime? endTime, TimeSpan duration, string pickUpLocation,
            string dropOffLocation, int seats, decimal tripCost )
        {
            var tripData = new Trip
            {
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration,
                PickUpLocation = pickUpLocation,
                DropOffLocation = dropOffLocation,
                Seats = seats,
                TripCost = tripCost
            };

            this.data.Trips.Add(tripData);
            this.data.SaveChanges();

            return tripData.Id;
        }
    }
}
