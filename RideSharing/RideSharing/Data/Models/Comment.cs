namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Comment
    {
        public int Id { get; set; }
        public int RiderId { get; set; }
        public Rider Rider { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
