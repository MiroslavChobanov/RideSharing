namespace RideSharing.Services.Riders
{
    public interface IRiderService
    {
        public int Join(
            string firstName,
            string lastName,
            string gender,
            string phoneNumber,
            string userId);
        public bool IsRider(string userId);

        public int IdByUser(string userId);

        public bool Edit(
            int riderId,
            string firstName,
            string lastName,
            string gender,
            string phoneNumber);
    }
}
