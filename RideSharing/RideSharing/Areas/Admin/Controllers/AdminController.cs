namespace RideSharing.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RideSharing.Services.Users;
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRole)]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService users;
        public AdminController(RoleManager<IdentityRole> roleManager, IUserService users)
        {
            this.roleManager = roleManager;
            this.users = users;
        }

    }
}
