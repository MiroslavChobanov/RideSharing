namespace RideSharing.Data.Models
{
    public class Driver
    {
        public int Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; init; } = new List<Vehicle>();

    }
}
