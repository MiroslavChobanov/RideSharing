namespace RideSharing.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RideSharing.Data.Models;

    public class RideSharingDbContext : IdentityDbContext
    {
        public DbSet<Comment> Comments { get; init; }
        public DbSet<Driver> Drivers { get; init; }
        public DbSet<Payment> Payments { get; init; }
        public DbSet<Rider> Riders { get; init; }
        public DbSet<RiderTrip> RiderTrips { get; init; }
        public DbSet<Trip> Trips { get; init; }
        public DbSet<Vehicle> Vehicles { get; init; }
        public DbSet<VehicleType> VehicleTypes { get; init; }
        public RideSharingDbContext(DbContextOptions<RideSharingDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<RiderTrip>()
                .HasKey(rt => new { rt.RiderId, rt.TripId });
            modelBuilder
                .Entity<RiderTrip>()
                .HasOne(rt => rt.Rider)
                .WithMany(r => r.RidersTrips)
                .HasForeignKey(rt => rt.RiderId);
            modelBuilder
                .Entity<RiderTrip>()
                .HasOne(rt => rt.Trip)
                .WithMany(t => t.RidersTrips)
                .HasForeignKey(rt => rt.TripId);

            modelBuilder
                .Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Vehicle>()
                .HasOne(v => v.Driver)
                .WithMany(d => d.Vehicles)
                .HasForeignKey(v => v.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Driver>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Driver>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Trip>()
                .HasOne(t => t.Driver)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Trip>()
                .HasOne(t => t.Vehicle)
                .WithMany(v => v.Trips)
                .HasForeignKey(t => t.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}