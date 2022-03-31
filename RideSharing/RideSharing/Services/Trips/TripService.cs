namespace RideSharing.Services.Trips
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using RideSharing.Models.Trips;
    using RideSharing.Services.Trips.Models;

    public class TripService : ITripService
    {
        private readonly RideSharingDbContext data;

        public TripService(RideSharingDbContext data)
        {
            this.data = data;
        }
        public List<TripDetailsServiceModel> All()
        {
            return this.data
                 .Trips
                 .Select(t => new TripDetailsServiceModel
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
            string dropOffLocation, int seats, decimal tripCost, int driverId, int vehicleId)
        {
            var tripData = new Trip
            {
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration,
                PickUpLocation = pickUpLocation,
                DropOffLocation = dropOffLocation,
                Seats = seats,
                TripCost = tripCost,
                DriverId = driverId,
                VehicleId = vehicleId
            };

            this.data.Trips.Add(tripData);
            this.data.SaveChanges();

            return tripData.Id;
        }

        public IEnumerable<TripVehicleServiceModel> AllVehicles()
        {
            return this.data
                .Vehicles
                .Select(v => new TripVehicleServiceModel
                {
                    Id = v.Id,
                    Brand = v.Brand,
                    Model = v.Model
                })
                .ToList();
        }
    }
}
