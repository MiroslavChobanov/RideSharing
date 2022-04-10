namespace RideSharing.Controllers
{
    using System.Linq;
    using RideSharing.Infrastructure;
    using RideSharing.Data;
    using RideSharing.Constants;
    using RideSharing.Models.Drivers;
    using RideSharing.Services.Drivers;
    using RideSharing.Services.Drivers.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Join() 
        {
            var userId = this.User.Id();

            var isDriver = this.drivers.IsDriver(userId);

            if (isDriver)
            {
                TempData[MessageConstants.ErrorMessage] = "You are already a driver!";
                return Redirect(Request.Path);
            }

            return View();
        }


        [HttpPost]
        [Authorize]
        public IActionResult Join(JoinAsDriverFormModel driver)
        {
            var userId = this.User.Id();

            var isDriver = this.drivers.IsDriver(userId);

            if (isDriver)
            {
                TempData[MessageConstants.ErrorMessage] = "You are already a driver!";
                return Redirect(Request.Path);
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

            TempData[MessageConstants.SuccessMessage] = "You are now a driver!";

            return RedirectToAction(nameof(VehiclesController.MyVehicles), "Vehicles");
        }

        [Authorize]
        public IActionResult Resign(string id)
        {
            var driverId = this.drivers.IdByUser(id);

            var driverForm = this.drivers.ResignViewData(driverId);

            return View(driverForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Resign(string id, DriverResignServiceModel model)
        {
            var driverId = this.drivers.IdByUser(id);

            var deleted = this.drivers.Resign(driverId);

            if (!deleted)
            {
                TempData[MessageConstants.ErrorMessage] = "Something went wrong!";
                return Redirect(Request.Path);
            }

            TempData[MessageConstants.SuccessMessage] = "You have resigned!";

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize]
        public IActionResult EditInformation(int id)
        {
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (driverId == 0)
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            var driverForm = this.data.Drivers
                .Where(d => d.Id == id)
                .Select(d => new DriverEditInformationServiceModel
                {
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Gender = d.Gender,
                    PhoneNumber = d.PhoneNumber
                })
                .FirstOrDefault();


            return View(driverForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditInformation(int id, DriverEditInformationServiceModel driver)
        {
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (driverId == 0)
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            var edited = this.drivers.Edit(
                id,
                driver.FirstName,
                driver.LastName,
                driver.Gender,
                driver.PhoneNumber
                );

            if (!edited)
            {
                TempData[MessageConstants.ErrorMessage] = "Something went wrong!";
                return Redirect(Request.Path);
            }

            TempData[MessageConstants.SuccessMessage] = "You have successfully edited your information!";

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
