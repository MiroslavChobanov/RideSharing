using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RideSharing.Data;
using RideSharing.Infrastructure;
using RideSharing.Services.Drivers;
using RideSharing.Services.Vehicles;
using RideSharing.Services.Trips;
using RideSharing.Services.Riders;
using RideSharing.Services.Comments;
using RideSharing.Services.Users;
using RideSharing.Hubs;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection");

builder
    .Services
    .AddDbContext<RideSharingDbContext>(options =>options
    .UseSqlServer(connectionString));

builder
    .Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder
    .Services
    .AddMemoryCache();

builder
    .Services
    .AddDefaultIdentity<IdentityUser>(options => 
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<RideSharingDbContext>();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
});

builder.Services.AddTransient<IVehicleService, VehicleService>();

builder.Services.AddTransient<IDriverService, DriverService>();

builder.Services.AddTransient<ITripService, TripService>();

builder.Services.AddTransient<IRiderService, RiderService>();

builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddSignalR();


var app = builder.Build();
    app.PrepareDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()    
    .UseAuthorization();

app.MapDefaultControllerRoute();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");
app.UseAuthentication();
app.Run();

 
