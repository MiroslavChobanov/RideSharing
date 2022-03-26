namespace RideSharing.Models.Drivers
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Driver;
    public class JoinAsDriverFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
