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
    using RideSharing.Data.Models;
    using RideSharing.Models.Vehicles;
    using RideSharing.Services.Vehicles;

    public class VehiclesController : Controller
    {
        private readonly RideSharingDbContext _context;
        private readonly IVehicleService vehicles;

        public VehiclesController(RideSharingDbContext context, IVehicleService vehicles)
        {
            _context = context;
            this.vehicles = vehicles;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var rideSharingDbContext = _context.Vehicles.Include(v => v.Driver).Include(v => v.VehicleType);
            return View(await rideSharingDbContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateVehicleFormModel vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicleId = this.vehicles.Create(
                    vehicle.Model,
                    vehicle.Make,
                    vehicle.YearOfCreation,
                    vehicle.LastServicingDate,
                    vehicle.ImagePath,
                    vehicle.VehicleTypeId);


            return Redirect("/Vehicles/Index");
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", vehicle.DriverId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Make,YearOfCreation,LastServicingDate,DriverId,VehicleTypeId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", vehicle.DriverId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
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
