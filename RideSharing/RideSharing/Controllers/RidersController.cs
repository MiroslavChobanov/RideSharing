namespace RideSharing.Controllers
{
    using RideSharing.Data;
    using RideSharing.Services.Riders;
    using RideSharing.Models.Riders;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using RideSharing.Infrastructure;

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


    }
}
