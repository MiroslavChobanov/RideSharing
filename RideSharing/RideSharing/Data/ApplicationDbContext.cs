namespace RideSharing.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RideSharing.Data.Models;

    public class RideSharingDbContext : IdentityDbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<RateTrip> RateTrips { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<RiderTrip> RiderTrips { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public RideSharingDbContext(DbContextOptions<RideSharingDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RateTrip>()
                .HasKey(rt => new { rt.RateId, rt.TripId });
            modelBuilder.Entity<RateTrip>()
                .HasOne(rt => rt.Rate)
                .WithMany(r => r.RatesTrips)
                .HasForeignKey(rt => rt.RateId);
            modelBuilder.Entity<RateTrip>()
                .HasOne(rt => rt.Trip)
                .WithMany(t => t.RatesTrips)
                .HasForeignKey(rt => rt.TripId);

            modelBuilder.Entity<RiderTrip>()
                .HasKey(rt => new { rt.RiderId, rt.TripId });
            modelBuilder.Entity<RiderTrip>()
                .HasOne(rt => rt.Rider)
                .WithMany(r => r.RidersTrips)
                .HasForeignKey(rt => rt.RiderId);
            modelBuilder.Entity<RiderTrip>()
                .HasOne(rt => rt.Trip)
                .WithMany(t => t.RidersTrips)
                .HasForeignKey(rt => rt.TripId);
        }
    }
}