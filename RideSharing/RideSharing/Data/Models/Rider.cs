﻿namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Rider
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string LuggageSize { get; set; }
        public IEnumerable<RiderTrip> RidersTrips { get; init; } = new List<RiderTrip>();
        public IEnumerable<Comment> Comments { get; init; } = new List<Comment>();
        public IEnumerable<Payment> Payments { get; init; } = new List<Payment>();


    }
}