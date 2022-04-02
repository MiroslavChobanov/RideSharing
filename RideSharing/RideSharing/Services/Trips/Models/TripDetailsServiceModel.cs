namespace RideSharing.Services.Trips.Models
{
    using System.ComponentModel.DataAnnotations;
    public class TripDetailsServiceModel
    {
        public int Id { get; init; }
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; init; }
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; init; }
        public TimeSpan Duration { get; init; }
        [Display(Name = "Pick-Up Location")]
        public string PickUpLocation { get; init; }
        [Display(Name = "Drop-Off Location")]
        public string DropOffLocation { get; init; }
        [Display(Name = "Driver Name")]
        public string DriverName { get; init; }
        [Display(Name = "Driver Vehicle")]
        public string DriverVehicle { get; init; }
        public int Seats { get; init; }
        [Display(Name = "Trip Cost")]
        public decimal TripCost { get; init; }
    }
}
