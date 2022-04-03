namespace RideSharing.Services.Drivers
{
    using RideSharing.Services.Drivers.Models;
    public interface IDriverService
    {
        public int Join(
            string firstName,
            string lastName,
            string gender,
            string phoneNumber,
            string userId);

        public bool IsDriver(string userId);

        public int IdByUser(string userId);

        DriverDetailsServiceModel Details(int vehicleId);

        public DriverResignServiceModel ResignViewData(int id);

        public bool Resign(int id);
    }
}
