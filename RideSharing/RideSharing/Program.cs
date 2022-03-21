using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RideSharing.Data;
using RideSharing.Services.Vehicles;

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
    .Services
    .AddControllersWithViews();

var app = builder.Build();

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
