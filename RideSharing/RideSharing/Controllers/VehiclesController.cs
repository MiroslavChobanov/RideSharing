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

    public class VehiclesController : Controller
    {
        private readonly RideSharingDbContext _context;
        private readonly IVehicleService vehicles;
        private readonly IDriverService drivers;

        public VehiclesController(RideSharingDbContext context, IVehicleService vehicles, IDriverService drivers)
        {
            _context = context;
            this.vehicles = vehicles;
            this.drivers = drivers;
        }

        // GET: Vehicles
        public async Task<IActionResult> All()
        {
            var rideSharingDbContext = _context.Vehicles.Include(v => v.Driver).Include(v => v.VehicleType);
            return View(await rideSharingDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = this.vehicles.Details(id);

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View(new CreateVehicleFormModel
            {
                VehicleTypes = this.GetCarVehicleType()
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
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.drivers.IsDriver(userId))
            {
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            var vehicle = this.vehicles.Details(id);

            if (vehicle.UserId != userId)
            {
                return Unauthorized();
            }

            return View(vehicle);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int vehicleId, CreateVehicleFormModel vehicle)
        {
            var driverId = this.drivers.IdByUser(this.User.Id());

            if (driverId == 0 )
            {
                return RedirectToAction(nameof(DriversController.Join), "Drivers");
            }

            if (!this.vehicles.VehicleTypeExists(vehicle.VehicleTypeId))
            {
                this.ModelState.AddModelError(nameof(vehicle.VehicleTypeId), "Vehicle Type does not exist.");
            }

            if (!this.vehicles.IsByDriver(vehicleId, driverId))
            {
                return BadRequest();
            }

            var vehicleEdited = this.vehicles.Edit(
                vehicleId,
                vehicle.Brand,
                vehicle.Model,
                vehicle.YearOfCreation,
                vehicle.LastServicingDate,
                vehicle.ImagePath,
                vehicle.VehicleTypeId);

            if (!vehicleEdited)
            {
                return BadRequest();
            }

            return Redirect("/Vehicles/All");
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Driver)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }

        private IEnumerable<CarVehicleTypeViewModel> GetCarVehicleType()
        {
            return this._context
                .VehicleTypes
                .Select(vt => new CarVehicleTypeViewModel
            {
                Id = vt.Id,
                Type = vt.Type
            })
            .ToList();
        }
    }
}
