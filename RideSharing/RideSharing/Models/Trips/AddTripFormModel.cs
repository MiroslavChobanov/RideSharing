namespace RideSharing.Models.Trips
{
    using RideSharing.Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Trip;
    public class AddTripFormModel
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        [Required]
        [StringLength(LocationMinLength, MinimumLength = LocationMaxLength)]
        public string PickUpLocation { get; set; }
        [Required]
        [StringLength(LocationMinLength, MinimumLength = LocationMaxLength)]
        public string DropOffLocation { get; set; }
        public int Seats { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public decimal TripCost { get; set; }
    }
}
