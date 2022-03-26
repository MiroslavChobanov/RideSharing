namespace RideSharing.Models.Vehicles
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using static Data.DataConstants.Vehicle;
    public class CreateVehicleFormModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; init; }
        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; init; }
        [Range(YearMinValue,YearMaxValue)]
        public int YearOfCreation { get; init; }
        public string LastServicingDate { get; init; }
        [Required]
        [Url]
        [Display(Name = "Image Path")]
        public string ImagePath { get; init; }
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; init; }
        public IEnumerable<CarVehicleTypeViewModel> VehicleTypes { get; set; }
    }
}
