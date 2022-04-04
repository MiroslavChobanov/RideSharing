using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RideSharing.Data;
using RideSharing.Infrastructure;
using RideSharing.Services.Drivers;
using RideSharing.Services.Vehicles;
using RideSharing.Services.Trips;
using RideSharing.Services.Riders;
using RideSharing.Services.Comments;

var builder = WebApplication.CreateBuilder(args);


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

builder
    .Services
    .AddDefaultIdentity<IdentityUser>(options => 
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<RideSharingDbContext>();

builder
    .Services.AddTransient<IVehicleService, VehicleService>();

builder
    .Services.AddTransient<IDriverService, DriverService>();

builder
    .Services.AddTransient<ITripService, TripService>();

builder
    .Services.AddTransient<IRiderService, RiderService>();

builder
    .Services.AddTransient<ICommentService, CommentService>();

builder
    .Services
    .AddControllersWithViews();

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
app.UseAuthentication();
app.Run();
