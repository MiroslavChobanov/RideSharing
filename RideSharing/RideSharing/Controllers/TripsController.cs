namespace RideSharing.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RideSharing.Data;
    using RideSharing.Models.Trips;
    using RideSharing.Services.Trips;
    using RideSharing.Services.Drivers;
    using RideSharing.Infrastructure;
    using RideSharing.Services.Trips.Models;
    using RideSharing.Data.Models;
    using RideSharing.Services.Riders;

    public class TripsController : Controller
    {
        private readonly RideSharingDbContext data;
        private readonly ITripService trips;
        private readonly IDriverService drivers;
        private readonly IRiderService riders;

        public TripsController(RideSharingDbContext data, ITripService trips,
            IDriverService drivers, IRiderService riders)
        {
            this.data = data;
            this.trips = trips;
            this.drivers = drivers;
            this.riders = riders;
        }
        public IActionResult All()
        {
            var trips = this.trips.All();

            return View(trips);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.drivers.IsDriver(this.User.Id()))
            {
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }
            return View(new AddTripFormModel
            {
                Vehicles = this.trips.AllVehicles()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTripFormModel trip)
        {
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (driverId == 0)
            {
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var tripId = this.trips.Add(
                    trip.StartTime,
                    trip.EndTime,
                    trip.Duration,
                    trip.PickUpLocation,
                    trip.DropOffLocation,
                    trip.Seats,
                    trip.TripCost,
                    driverId,
                    trip.VehicleId
                    );


            return Redirect("/Trips/All");
        }

        public async Task<IActionResult> Details(int id)
        {
            var trip = this.trips.Details(id);

            return View(trip);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.drivers.IsDriver(userId))
            {
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            var trip = this.trips.Details(id);

            var tripForm = this.data.Trips
                .Where(t => t.Id == id)
                .Select(t => new EditTripFormModel
                {
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Duration = t.Duration,
                    PickUpLocation = t.PickUpLocation,
                    DropOffLocation = t.DropOffLocation,
                    Seats = t.Seats,
                    TripCost = t.TripCost
                })
                .FirstOrDefault();


            return View(tripForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, EditTripFormModel trip)
        {

            var edited = this.trips.Edit(
                id,
                trip.StartTime,
                trip.EndTime,
                trip.Duration,
                trip.PickUpLocation,
                trip.DropOffLocation,
                trip.Seats,
                trip.TripCost
                );

            if (!edited)
            {
                return BadRequest();
            }

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Postpone(int id)
        {

            var trip = this.trips.Details(id);

            var tripForm = this.trips.PostponeViewData(trip.Id);

            return View(tripForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Postpone(int id, TripPostponeServiceModel trip)
        {

            var deleted = this.trips.Postpone(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Join(int id)
        {
            var riderId = this.riders.IdByUser(this.User.Id());

            var riderTrip = new RiderTrip
            {
                RiderId = riderId,
                TripId = id
            };

            var trip = data
                .Trips
                .FirstOrDefault(t => t.Id == id);

            trip.Seats -= 1;


            this.data.RiderTrips.Add(riderTrip);

            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }
    }
}
