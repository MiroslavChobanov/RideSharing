namespace RideSharing.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using RideSharing.Data;
    using RideSharing.Constants;
    using RideSharing.Models.Vehicles;
    using RideSharing.Services.Vehicles;
    using RideSharing.Services.Drivers;
    using Microsoft.AspNetCore.Authorization;
    using RideSharing.Infrastructure;
    using RideSharing.Services.Vehicles.Models;

    public class VehiclesController : Controller
    {
        private readonly RideSharingDbContext data;
        private readonly IVehicleService vehicles;
        private readonly IDriverService drivers;

        public VehiclesController(RideSharingDbContext data, IVehicleService vehicles, IDriverService drivers)
        {
            this.data = data;
            this.vehicles = vehicles;
            this.drivers = drivers;
        }

        [Authorize]
        public IActionResult MyVehicles()
        {
            var myVehicles = this.vehicles.ByUser(this.User.Id());

            return View(myVehicles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = this.vehicles.Details(id);

            return View(vehicle);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.drivers.IsDriver(this.User.Id()))
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }
            return View(new AddVehicleFormModel
            {
                VehicleTypes = this.vehicles.AllVehicleTypes()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddVehicleFormModel vehicle)
        {
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (driverId == 0)
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!this.vehicles.VehicleTypeExists(vehicle.VehicleTypeId))
            {
                TempData[MessageConstants.ErrorMessage] = "Vehicle type does not exist!";
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicleId = this.vehicles.Add(
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.YearOfCreation,
                    vehicle.LastServicingDate,
                    vehicle.ImagePath,
                    vehicle.VehicleTypeId,
                    driverId);

            TempData[MessageConstants.SuccessMessage] = "Your vehicle was added successfully!";

            return RedirectToAction("MyVehicles");
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.drivers.IsDriver(userId))
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            var vehicle = this.vehicles.Details(id);

            var vehicleForm = this.data.Vehicles
                .Where(v => v.Id == id)
                .Select(v => new AddVehicleFormModel
                {
                    Brand = v.Brand,
                    Model = v.Model,
                    YearOfCreation = v.YearOfCreation,
                    LastServicingDate = v.LastServicingDate,
                    ImagePath = v.ImagePath,
                    VehicleTypeId = v.VehicleTypeId
                })
                .FirstOrDefault();

            vehicleForm.VehicleTypes = this.vehicles.AllVehicleTypes();

            return View(vehicleForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, AddVehicleFormModel vehicle)
        {
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (driverId == 0)
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!this.vehicles.VehicleTypeExists(vehicle.VehicleTypeId))
            {
                TempData[MessageConstants.ErrorMessage] = "Vehicle type does not exist!";
            }

            if (!ModelState.IsValid)
            {
                vehicle.VehicleTypes = this.vehicles.AllVehicleTypes();

                return View(vehicle);
            }

            if (!this.vehicles.IsByDealer(id, driverId) )
            {
                TempData[MessageConstants.ErrorMessage] = "This is not your vehicle!";
                return Redirect(Request.Path);
            }

            var edited = this.vehicles.Edit(
                id,
                vehicle.Brand,
                vehicle.Model,
                vehicle.YearOfCreation,
                vehicle.LastServicingDate,
                vehicle.ImagePath,
                vehicle.VehicleTypeId
                );

            if (!edited)
            {
                TempData[MessageConstants.ErrorMessage] = "Something went wrong!";
                return Redirect(Request.Path);
            }

            TempData[MessageConstants.SuccessMessage] = "Your vehicle was edited successfully!";

            return RedirectToAction("MyVehicles");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = this.User.Id();

            if (!this.drivers.IsDriver(userId))
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            var vehicle = this.vehicles.Details(id);

            var vehicleForm = this.vehicles.DeleteViewData(vehicle.Id);

            return View(vehicleForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id, VehicleDeleteServiceModel vehicle)
        {
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (driverId == 0)
            {
                TempData[MessageConstants.ErrorMessage] = "You are not a driver!";
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!this.vehicles.IsByDealer(id, driverId))
            {
                TempData[MessageConstants.ErrorMessage] = "This is not your vehicle!";
                return Redirect(Request.Path);
            }

            var deleted = this.vehicles.Delete(id);

            if (!deleted)
            {
                TempData[MessageConstants.ErrorMessage] = "Something went wrong!";
                return Redirect(Request.Path);
            }

            TempData[MessageConstants.SuccessMessage] = "Your vehicle was deleted successfully!";

            return RedirectToAction("MyVehicles");
        }

        private bool VehicleExists(int id)
        {
            return data.Vehicles.Any(e => e.Id == id);
        }

        private IEnumerable<VehicleVehicleTypeViewModel> GetVehicleVehicleType()
        {
            return this.data
                .VehicleTypes
                .Select(vt => new VehicleVehicleTypeViewModel
            {
                Id = vt.Id,
                Type = vt.Type
            })
            .ToList();
        }
    }
}
