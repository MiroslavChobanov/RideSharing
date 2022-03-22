namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class VehicleType
    {
        public int Id { get; init; }
        [Required]
        public string Type { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
