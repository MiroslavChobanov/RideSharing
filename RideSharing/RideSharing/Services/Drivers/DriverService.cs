namespace RideSharing.Services.Drivers
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    public class DriverService : IDriverService
    {
        private readonly RideSharingDbContext data;

        public DriverService(RideSharingDbContext data)
        {
            this.data = data;
        }
        public int Join(string firstName, string lastName, string gender, string phoneNumber, string userId)
        {
            var driverData = new Driver
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                PhoneNumber = phoneNumber,
                UserId = userId
            };

            this.data.Drivers.Add(driverData);
            this.data.SaveChanges();

            return driverData.Id;
        }

        public bool IsDriver(string userId)
        {
            return this.data
                .Drivers
                .Any(d => d.UserId == userId);
        }

        public int IdByUser(string userId)
        {
            return this.data
                .Drivers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
        }
    }
}
