namespace RideSharing.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RideSharing.Services.Users;

    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService users;
        public AdminController(RoleManager<IdentityRole> roleManager, IUserService users)
        {
            this.roleManager = roleManager;
            this.users = users;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
