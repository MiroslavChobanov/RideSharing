namespace RideSharing.Controllers
{
    using System.Linq;
    using RideSharing.Infrastructure;
    using RideSharing.Data;
    using RideSharing.Models.Drivers;
    using RideSharing.Services.Drivers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    public class DriversController : Controller
    {
        private readonly RideSharingDbContext data;
        private readonly IDriverService drivers;

        public DriversController(RideSharingDbContext data, IDriverService drivers)
        {
            this.data = data;
            this.drivers = drivers;
        }

        [Authorize]
        public IActionResult Join() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Join(JoinAsDriverFormModel driver)
        {
            var userId = this.User.Id();

            if (this.data.Drivers.Any(d => d.UserId == userId))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(driver);
            }

            var driverId = this.drivers.Join(
                    driver.FirstName,
                    driver.LastName,
                    driver.Gender,
                    driver.PhoneNumber,
                    userId);

            TempData[GlobalMessageKey] = "Thank you for becoming a driver!";

            return RedirectToAction(nameof(VehiclesController.All), "Vehicles");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {

            var driver = this.drivers.Details(id);

            var driverForm = this.drivers.ResignViewData(driver.Id);

            return View(driverForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Resign(int id)
        {

            var deleted = this.drivers.Resign(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return RedirectToAction("Index");
        }
    }
}
