namespace RideSharing.Models.Trips
{
    using System.ComponentModel.DataAnnotations;
    public class EditTripFormModel
    {
        public int Id { get; set; }
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }
        public TimeSpan Duration { get; set; }

        [Display(Name = "Pick Up Location")]
        public string PickUpLocation { get; set; }

        [Display(Name = "Drop Off Location")]
        public string DropOffLocation { get; set; }
        public int Seats { get; set; }
        [Display(Name = "Driver Name")]
        public decimal TripCost { get; set; }
        public int DriverId { get; set; }
    }
}
