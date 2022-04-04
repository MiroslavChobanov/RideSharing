namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Comment
    {
        public int Id { get; set; }
        public int RiderId { get; set; }
        public Rider Rider { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime? LastEditedOn { get; set; }
    }
}
