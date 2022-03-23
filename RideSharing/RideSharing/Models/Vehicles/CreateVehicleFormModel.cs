namespace RideSharing.Models.Vehicles
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class CreateVehicleFormModel
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Model { get; set; }
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Make { get; set; }
        public string YearOfCreation { get; set; }
        public string LastServicingDate { get; set; }

        public int VehicleTypeId { get; set; }
    }
}
