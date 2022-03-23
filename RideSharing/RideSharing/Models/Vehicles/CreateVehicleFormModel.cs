namespace RideSharing.Models.Vehicles
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using static Data.DataConstants;
    public class CreateVehicleFormModel
    {
        public int Id { get; init; }
        public string Model { get; init; }
        public string Make { get; init; }
        public string YearOfCreation { get; init; }
        public string LastServicingDate { get; init; }
        [Display(Name = "Image Path")]
        public string ImagePath { get; init; }
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; init; }
        public IEnumerable<CarVehicleTypeViewModel> VehicleTypes { get; set; }
    }
}
