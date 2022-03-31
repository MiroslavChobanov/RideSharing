namespace RideSharing.Models.Trips
{
    using RideSharing.Data.Models;
    using RideSharing.Services.Trips.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Trip;
    public class AddTripFormModel
    {
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        [Required]
        [StringLength(LocationMaxLength, MinimumLength = LocationMinLength)]
        [Display(Name = "Pick Up Location")]
        public string PickUpLocation { get; set; }
        [Required]
        [StringLength(LocationMaxLength, MinimumLength = LocationMinLength)]
        [Display(Name = "Drop Off Location")]
        public string DropOffLocation { get; set; }
        public int Seats { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }
        public IEnumerable<TripVehicleServiceModel> Vehicles { get; set; }
        [Display(Name = "Trip Cost")]
        public decimal TripCost { get; set; }
    }
}
