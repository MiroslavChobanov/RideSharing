namespace RideSharing.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RideSharing.Data.Models;

    public class RideSharingDbContext : IdentityDbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public RideSharingDbContext(DbContextOptions<RideSharingDbContext> options)
            : base(options)
        {

        }
    }
}