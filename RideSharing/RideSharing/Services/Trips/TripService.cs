namespace RideSharing.Services.Trips
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using RideSharing.Models.Comments;
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
                     Id = t.Id,
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

        public TripDetailsServiceModel Details(int id)
        {
            return this.data.Trips
                .Where(t => t.Id == id)
                .Select(t => new TripDetailsServiceModel
                {
                    Id = t.Id,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Duration = t.Duration,
                    PickUpLocation = t.PickUpLocation,
                    DropOffLocation = t.DropOffLocation,
                    DriverName = t.Driver.FirstName + " " + t.Driver.LastName,
                    DriverVehicle = t.Vehicle.Brand + " " + t.Vehicle.Model,
                    Seats = t.Seats,
                    TripCost = t.TripCost,
                    Comments = t.Comments
                        .Select(c => new CommentListingModel
                        {
                            Id = c.Id,
                            Description = c.Description,
                            Date = c.Date,
                            LastEditedOn = c.LastEditedOn,
                            TripId = c.TripId,
                            RiderId = c.RiderId,
                            Rider = c.Rider,
                        })
                            .OrderByDescending(x => x.Date)
                            .ToList(),
                        })
                .First();
        }

        public bool Edit(
            int tripId,
            DateTime startTime,
            DateTime? endTime,
            TimeSpan duration,
            string pickUpLocation,
            string dropOffLocation,
            int seats,
            decimal TripCost)
        {
            var tripData = this.data.Trips.Find(tripId);

            if (tripData == null)
            {
                return false;
            }

            tripData.StartTime = startTime;
            tripData.EndTime = endTime;
            tripData.Duration = duration;
            tripData.PickUpLocation = pickUpLocation;
            tripData.DropOffLocation = dropOffLocation;
            tripData.Seats = seats;
            tripData.TripCost = TripCost;

            this.data.SaveChanges();

            return true;
        }

        public TripPostponeServiceModel PostponeViewData(int id)
        {
            return this.data.Trips
                .Where(t => t.Id == id)
                .Select(t => new TripPostponeServiceModel
                {
                    StartTime = t.StartTime,
                    PickUpLocation = t.PickUpLocation,
                    DropOffLocation = t.DropOffLocation
                })
                .First();
        }

        public bool Postpone(int id)
        {
            var tripData = this.data.Trips.Find(id);

            if (tripData == null)
            {
                return false;
            }

            this.data.Trips.Remove(tripData);
            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<TripDetailsServiceModel> ByUser(string userId)
        {
            return GetTrips(this.data
                .Trips
                .Where(c => c.Driver.UserId == userId));
        }

        private IEnumerable<TripDetailsServiceModel> GetTrips(IQueryable<Trip> tripQuery)
        {
            return tripQuery
                .Select(t => new TripDetailsServiceModel
                {
                    Id = t.Id,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Duration = t.Duration,
                    PickUpLocation = t.PickUpLocation,
                    DropOffLocation = t.DropOffLocation,
                    DriverVehicle = t.Vehicle.Brand + " " + t.Vehicle.Model,
                    Seats = t.Seats,
                    TripCost = t.TripCost,
                    Comments = t.Comments
                        .Select(c => new CommentListingModel
                    {
                        Id = c.Id,
                        Description = c.Description,
                        Date = c.Date,
                        LastEditedOn = c.LastEditedOn,
                        TripId = c.TripId,
                        RiderId = c.RiderId,
                        Rider = c.Rider,
                    })
                            .OrderByDescending(x => x.Date)
                            .ToList(),
                })
                .ToList();
        }

        public void ChangeVisibility(int tripId)
        {
            var trip = this.data.Trips.Find(tripId);

            trip.isPublic = !trip.isPublic;

            this.data.SaveChanges();
        }

        public bool IsByDriver(int tripId, int driverId)
        {
            return this.data
                .Trips
                .Any(t => t.Id == tripId && t.DriverId == driverId);
        }
    }
}
