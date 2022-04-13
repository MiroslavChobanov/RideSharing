namespace RideSharing.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RideSharing.Services.Users;

    public class UsersController : Controller
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            return View();
        }
        [Authorize]
        public IActionResult RiderProfile(string id)
        {

            var detailsForm = this.users.RiderDetails(id);

            return View(detailsForm);
        }
        [Authorize]
        public IActionResult DriverProfile(string id)
        {

            var detailsForm = this.users.DriverDetails(id);

            return View(detailsForm);
        }

        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }
    }
}
