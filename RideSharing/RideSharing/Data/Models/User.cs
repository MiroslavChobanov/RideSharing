namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static Data.DataConstants;
    public class User : IdentityUser
    {
        [MaxLength(DefaultMaxLength)]
        public string FirstName { get; set; }
        [MaxLength(DefaultMaxLength)]
        public string LastName { get; set; }
    }
}
