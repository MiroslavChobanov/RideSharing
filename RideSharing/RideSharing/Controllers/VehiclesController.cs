namespace RideSharing.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using RideSharing.Data;
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

        public IActionResult All()
        {
            var vehicles = this.vehicles.All();

            return View(vehicles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = this.vehicles.Details(id);

            return View(vehicle);
        }

        [Authorize]
        public IActionResult Create()
        {
            if (!this.drivers.IsDriver(this.User.Id()))
            {
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }
            return View(new CreateVehicleFormModel
            {
                VehicleTypes = this.vehicles.AllVehicleTypes()
            });
        }

        // POST: Vehicles/Create
        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateVehicleFormModel vehicle)
        {
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (driverId == 0)
            {
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!this.vehicles.VehicleTypeExists(vehicle.VehicleTypeId))
            {
                this.ModelState.AddModelError(nameof(vehicle.VehicleTypeId), "Vehicle type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicleId = this.vehicles.Create(
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.YearOfCreation,
                    vehicle.LastServicingDate,
                    vehicle.ImagePath,
                    vehicle.VehicleTypeId,
                    driverId);


            return Redirect("/Vehicles/All");
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.drivers.IsDriver(userId))
            {
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            var vehicle = this.vehicles.Details(id);

            var vehicleForm = this.data.Vehicles
                .Where(v => v.Id == id)
                .Select(v => new CreateVehicleFormModel
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
        public IActionResult Edit(int id, CreateVehicleFormModel vehicle)
        {
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (driverId == 0)
            {
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!this.vehicles.VehicleTypeExists(vehicle.VehicleTypeId))
            {
                this.ModelState.AddModelError(nameof(vehicle.VehicleTypeId), "Vehicle Type does not exist.");
            }

            if (!ModelState.IsValid)
            {
                vehicle.VehicleTypes = this.vehicles.AllVehicleTypes();

                return View(vehicle);
            }

            if (!this.vehicles.IsByDealer(id, driverId) )
            {
                return BadRequest();
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
                return BadRequest();
            }

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = this.User.Id();

            if (!this.drivers.IsDriver(userId))
            {
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
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!this.vehicles.IsByDealer(id, driverId))
            {
                return BadRequest();
            }

            var deleted = this.vehicles.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return RedirectToAction("All");
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
