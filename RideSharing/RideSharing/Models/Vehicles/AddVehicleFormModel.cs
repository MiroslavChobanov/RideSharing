namespace RideSharing.Models.Vehicles
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using static Data.DataConstants.Vehicle;
    using RideSharing.Services.Vehicles.Models;

    public class AddVehicleFormModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; init; }
        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; init; }
        [Range(YearMinValue,YearMaxValue)]
        [Display(Name = "Year Of Creation")]
        public int YearOfCreation { get; init; }
        [DataType(DataType.Date)]
        [Display(Name = "Last Serviced On")]
        public DateTime LastServicingDate { get; init; }
        [Required]
        [Url]
        [Display(Name = "Image Path")]
        public string ImagePath { get; init; }
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; init; }
        public IEnumerable<VehicleVehicleTypeServiceModel> VehicleTypes { get; set; }
    }
}
