namespace RideSharing.Services.Riders
{
    using RideSharing.Data;
    using RideSharing.Data.Models;
    public class RiderService : IRiderService
    {
        private readonly RideSharingDbContext data;

        public RiderService(RideSharingDbContext data)
        {
            this.data = data;
        }
        public int Join(string firstName, string lastName, string gender, string phoneNumber, string userId)
        {
            var riderData = new Rider
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                PhoneNumber = phoneNumber,
                UserId = userId
            };

            this.data.Riders.Add(riderData);
            this.data.SaveChanges();

            return riderData.Id;
        }
        public bool IsRider(string userId)
        {
            return this.data
                .Riders
                .Any(r => r.UserId == userId);
        }
        public int IdByUser(string userId)
        {
            return this.data
                .Riders
                .Where(r => r.UserId == userId)
                .Select(r => r.Id)
                .FirstOrDefault();
        }

        public bool Edit(
            int riderId,
            string firstName,
            string lastName,
            string gender,
            string phoneNumber)
        {
            var riderData = this.data.Riders.Find(riderId);

            if (riderData == null)
            {
                return false;
            }

            riderData.FirstName = firstName;
            riderData.LastName = lastName;
            riderData.Gender = gender;
            riderData.PhoneNumber = phoneNumber;

            this.data.SaveChanges();

            return true;
        }
    }
}
