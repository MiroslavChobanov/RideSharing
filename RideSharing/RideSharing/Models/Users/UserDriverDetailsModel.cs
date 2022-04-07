namespace RideSharing.Models.Users
{
    using RideSharing.Data.Models;
    public class UserDriverDetailsModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public ICollection<Trip> Trips { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
