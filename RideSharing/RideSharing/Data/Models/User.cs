namespace RideSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.User;
    using Microsoft.AspNetCore.Identity;
    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FirstName { get; set; }
        [MaxLength(FullNameMaxLength)]
        public string LastName { get; set; }
    }
}
