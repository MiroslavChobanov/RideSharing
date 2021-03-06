namespace RideSharing.Controllers
{
    using System.Collections.Generic;
    using RideSharing.Data;
    using RideSharing.Models.Trips;
    using RideSharing.Models.Comments;
    using RideSharing.Services.Trips;
    using RideSharing.Services.Drivers;
    using RideSharing.Infrastructure;
    using RideSharing.Services.Trips.Models;
    using RideSharing.Data.Models;
    using RideSharing.Services.Riders;
    using RideSharing.Services.Comments;
    using RideSharing.Constants;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TripsController : Controller
    {
        private readonly RideSharingDbContext data;
        private readonly ITripService trips;
        private readonly IDriverService drivers;
        private readonly IRiderService riders;
        private readonly ICommentService comments;
        private readonly IMemoryCache cache;

        public TripsController(RideSharingDbContext data, ITripService trips,
            IDriverService drivers, IRiderService riders, ICommentService comments, IMemoryCache cache)
        {
            this.data = data;
            this.trips = trips;
            this.drivers = drivers;
            this.riders = riders;
            this.comments = comments;
            this.cache = cache;
        }
        public IActionResult All()
        {
            const string latestNewsCacheKey = "latestNewsCacheKey";

            //if (!this.riders.IsRider(this.User.Id()))
            //{
            //    TempData[MessageConstants.ErrorMessage] = "You are not a rider!";
            //    return RedirectToAction(nameof(RidersController.Join), "Riders");
            //}

            var trips = this.cache.Get<List<TripDetailsServiceModel>>(latestNewsCacheKey);

            if (trips == null)
            {
                trips = this.trips.All();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                this.cache.Set(latestNewsCacheKey, trips, cacheOptions);
            }

            return View(trips);
        }

        [Authorize]
        public IActionResult MyTrips()
        {
            var myTrips = this.trips.ByUser(this.User.Id());

            return View(myTrips);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.drivers.IsDriver(this.User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
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
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
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

            TempData[MessageConstants.SuccessMessage] = "Successfully added trip!";

            return Redirect("/Trips/MyTrips");
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
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (!this.drivers.IsDriver(userId))
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            var trip = this.trips.Details(id);

            if (!this.trips.IsByDriver(id, driverId))
            {
                TempData[MessageConstants.ErrorMessage] = "This is not your trip!";
                return RedirectToAction(nameof(TripsController.All), "Trips");
            }

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
            var userId = this.User.Id();
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (!this.drivers.IsDriver(userId))
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if(!this.trips.IsByDriver(id, driverId))
            {
                TempData[MessageConstants.ErrorMessage] = "This is not your trip!";
                return RedirectToAction(nameof(TripsController.All), "Trips");
            }

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
                TempData[MessageConstants.ErrorMessage] = "Something went wrong!";
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Successfully edited trip!";

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Postpone(int id)
        {
            var userId = this.User.Id();
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (!this.drivers.IsDriver(userId))
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!this.trips.IsByDriver(id, driverId))
            {
                TempData[MessageConstants.ErrorMessage] = "This is not your trip!";
                return RedirectToAction(nameof(TripsController.All), "Trips");
            }

            var currTrip = data
                .Trips
                .FirstOrDefault(t => t.Id == id);

            if (currTrip.RidersTrips.Count() > 0)
            {
                TempData[MessageConstants.ErrorMessage] = "You cannot delete the trip!";
                return RedirectToAction(nameof(TripsController.MyTrips), "Trips");
            }

            var trip = this.trips.Details(id);

            var tripForm = this.trips.PostponeViewData(trip.Id);

            return View(tripForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Postpone(int id, TripPostponeServiceModel trip)
        {
            var userId = this.User.Id();
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (!this.drivers.IsDriver(userId))
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!this.trips.IsByDriver(id, driverId))
            {
                TempData[MessageConstants.ErrorMessage] = "This is not your trip!";
                return RedirectToAction(nameof(TripsController.All), "Trips");
            }

            var currTrip = data
                .Trips
                .FirstOrDefault(t => t.Id == id);

            if (currTrip.RidersTrips.Count() > 0)
            {
                TempData[MessageConstants.ErrorMessage] = "You cannot delete the trip!";
                return RedirectToAction(nameof(TripsController.MyTrips), "Trips");
            }

            var deleted = this.trips.Postpone(id);

            if (!deleted)
            {
                TempData[MessageConstants.ErrorMessage] = "Something went wrong!";
                return RedirectToAction(nameof(TripsController.MyTrips), "Trips");
            }

            TempData[MessageConstants.SuccessMessage] = "Successfully postponed trip!";

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

            if (trip.Seats == 0)
            {
                TempData[MessageConstants.ErrorMessage] = "There is no more space!";
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "You have successfully joined the trip!";

            this.data.RiderTrips.Add(riderTrip);
           
            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(int id, AddCommentFormModel comment)
        {
            var userId = this.riders.IdByUser(this.User.Id());

            //if (userId == 0)
            //{
            //    TempData[MessageConstants.ErrorMessage] = "You are not a rider!";
            //    return Redirect(Request.Path);
            //}

            var edited = this.comments.AddCommentToTrip(
                comment.Description,
                DateTime.Now,
                userId,
                id);

            TempData[MessageConstants.SuccessMessage] = "You have successfully added a comment!";

            return Redirect(Request.Path);
        }
    }
}
