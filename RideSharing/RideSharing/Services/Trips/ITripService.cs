namespace RideSharing.Services.Trips
{
    using RideSharing.Models.Trips;
    using RideSharing.Services.Trips.Models;
    public interface ITripService
    {
        public List<TripDetailsServiceModel> All();

         public int Add(DateTime startTime, DateTime? endTime, TimeSpan duration, string pickUpLocation,
            string dropOffLocation, int seats, decimal tripCost, int driverId, int vehicleId);

        public IEnumerable<TripVehicleServiceModel> AllVehicles();

        TripDetailsServiceModel Details(int tripId);

        bool Edit(
            int tripId,
            DateTime startTime,
            DateTime? endTime,
            TimeSpan duration,
            string pickUpLocation,
            string dropOffLocation,
            int seats,
            decimal TripCost);

        public TripPostponeServiceModel PostponeViewData(int id);
        public bool Postpone(int id);
    }
}
