namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.VehicleType;
    public class VehicleType
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(TypeMaxLength)]
        public string Type { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; init; } = new List<Vehicle>();
    }
}
