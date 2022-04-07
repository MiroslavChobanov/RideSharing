namespace RideSharing.Services.Users
{
    using RideSharing.Models.Users;
    public interface IUserService
    {
        public UserRiderDetailsModel RiderDetails(string id);
        public UserDriverDetailsModel DriverDetails(string id);
    }
}
