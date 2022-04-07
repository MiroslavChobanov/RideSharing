namespace RideSharing.Services.Users
{
    using RideSharing.Data;
    using RideSharing.Models.Users;
    using RideSharing.Services.Riders;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using RideSharing.Infrastructure;

    public class UserService : IUserService
    {
        private readonly RideSharingDbContext data;
        private readonly IRiderService riders;

        public UserService(RideSharingDbContext data, IRiderService riders)
        {
            this.data = data;
            this.riders = riders;
        }

        public UserRiderDetailsModel RiderDetails(string id)
        {
            var riderId = this.data
                .Riders
                .Where(r => r.UserId == id)
                .Select(r => r.Id)
                .FirstOrDefault();


            return this.data.Users
                .Where(u => u.Id == id)
                .Select(u => new UserRiderDetailsModel
                {
                    Id = id,
                    Email = u.Email,
                    Username = u.UserName,
                    Role = this.data.Roles.First(u => u.Id == this.data.UserRoles.First(u => u.UserId == id).RoleId).Name,
                    Trips = this.data.Trips
                    .Where(t => t.Id == this.data.RiderTrips
                    .Where(rt => rt.RiderId == riderId)
                    .Select(rt => rt.TripId)
                    .FirstOrDefault()).ToList(),
                    Comments = this.data.Comments.Where(c => c.RiderId == riderId).ToList()
                })
                .First();
        }

        public UserDriverDetailsModel DriverDetails(string id)
        {
            var driverId = this.data
                .Drivers
                .Where(r => r.UserId == id)
                .Select(r => r.Id)
                .FirstOrDefault();


            return this.data.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDriverDetailsModel
                {
                    Id = id,
                    Email = u.Email,
                    Username = u.UserName,
                    Role = this.data.Roles.First(u => u.Id == this.data.UserRoles.First(u => u.UserId == id).RoleId).Name,
                    Trips = this.data.Trips
                    .Where(t => t.DriverId == driverId).ToList(),
                    Vehicles = this.data.Vehicles.Where(v => v.DriverId == driverId).ToList()
                })
                .First();
        }
    }
}
