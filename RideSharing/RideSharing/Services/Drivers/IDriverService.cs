namespace RideSharing.Services.Drivers
{
    public interface IDriverService
    {
        int Join(
            string firstName,
            string lastName,
            string gender,
            string phoneNumber);
    }
}
