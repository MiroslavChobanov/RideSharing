namespace RideSharing.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RideSharing.Data;
    using RideSharing.Models.Trips;
    using RideSharing.Services.Trips;
    using RideSharing.Services.Drivers;
    using RideSharing.Infrastructure;

    public class TripsController : Controller
    {
        private readonly RideSharingDbContext data;
        private readonly ITripService trips;
        private readonly IDriverService drivers;

        public TripsController(RideSharingDbContext data, ITripService trips, IDriverService drivers)
        {
            this.data = data;
            this.trips = trips;
            this.drivers = drivers;
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
    }
}
