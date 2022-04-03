namespace RideSharing.Services.Drivers
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    using RideSharing.Services.Drivers.Models;
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

        public DriverDetailsServiceModel Details(int id)
        {
            return this.data.Drivers
                .Where(d => d.Id == id)
                .Select(d => new DriverDetailsServiceModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName
                })
                .First();
        }

        public DriverResignServiceModel ResignViewData(int id)
        {
            return this.data.Drivers
                .Where(d => d.Id == id)
                .Select(d => new DriverResignServiceModel
                {
                    FirstName = d.FirstName,
                    LastName = d.LastName
                })
                .First();
        }

        public bool Resign(int id)
        {
            var driverData = this.data.Drivers.Find(id);

            if (driverData == null)
            {
                return false;
            }

            this.data.Drivers.Remove(driverData);
            this.data.SaveChanges();

            return true;
        }
    }
}
