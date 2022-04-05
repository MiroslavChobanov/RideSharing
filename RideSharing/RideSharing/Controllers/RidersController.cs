﻿namespace RideSharing.Controllers
{
    using RideSharing.Data;
    using RideSharing.Services.Riders;
    using RideSharing.Models.Riders;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using RideSharing.Infrastructure;
    using RideSharing.Services.Riders.Models;

    public class RidersController : Controller
    {
        private readonly RideSharingDbContext data;
        private readonly IRiderService riders;

        public RidersController(RideSharingDbContext data, IRiderService riders)
        {
            this.data = data;
            this.riders = riders;
        }

        [Authorize]
        public IActionResult Join() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Join(JoinAsRiderFormModel rider)
        {
            var userId = this.User.Id();

            if (this.data.Riders.Any(r => r.UserId == userId))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(rider);
            }

            var riderId = this.riders.Join(
                    rider.FirstName,
                    rider.LastName,
                    rider.Gender,
                    rider.PhoneNumber,
                    userId);

            return RedirectToAction(nameof(TripsController.All), "Trips");
        }

        [Authorize]
        public IActionResult EditInformation(int id)
        {
            var riderId = this.riders.IdByUser(this.User.Id());

            if (riderId == 0)
            {
                return RedirectToAction(nameof(RidersController.Join), "Riders");
            }

            var riderForm = this.data.Riders
                .Where(r => r.Id == id)
                .Select(r => new RiderEditInformationServiceModel
                {
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Gender = r.Gender,
                    PhoneNumber = r.PhoneNumber
                })
                .FirstOrDefault();


            return View(riderForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditInformation(int id, RiderEditInformationServiceModel rider)
        {
            var riderId = this.riders.IdByUser(this.User.Id());

            if (riderId == 0)
            {
                return RedirectToAction(nameof(RidersController.Join), "Riders");
            }

            var edited = this.riders.Edit(
                id,
                rider.FirstName,
                rider.LastName,
                rider.Gender,
                rider.PhoneNumber
                );

            if (!edited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
