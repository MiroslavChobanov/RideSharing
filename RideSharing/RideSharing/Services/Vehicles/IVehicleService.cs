namespace RideSharing.Services.Vehicles
{
    public interface IVehicleService
    {
        int Create(
            string model,
            string make,
            string yearOfCreation,
            string lastServicingDate);
    }
}
