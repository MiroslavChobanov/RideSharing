namespace RideSharing.Services.Drivers
{
    public interface IDriverService
    {
        public int Join(
            string firstName,
            string lastName,
            string gender,
            string phoneNumber);

        public bool IsDriver(string userId);

        public int IdByUser(string userId);
    }
}
