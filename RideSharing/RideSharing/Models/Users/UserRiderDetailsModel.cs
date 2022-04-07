namespace RideSharing.Models.Users
{
    using RideSharing.Data.Models;

    public class UserRiderDetailsModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public ICollection<Trip> Trips { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
