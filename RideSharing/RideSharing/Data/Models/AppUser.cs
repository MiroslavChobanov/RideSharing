namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    using Microsoft.AspNetCore.Identity;
    public class AppUser : IdentityUser
    {
        [MaxLength(DefaultMaxLength)]
        public string? FirstName { get; set; }
        [MaxLength(DefaultMaxLength)]
        public string? LastName { get; set; }
    }
}
