namespace RideSharing.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RideSharing.Data;
    using RideSharing.Data.Models;

    using static Areas.Admin.AdminConstants;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedVehicleTypes(services);
            //SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<RideSharingDbContext>();

            data.Database.Migrate();
        }

        private static void SeedVehicleTypes(IServiceProvider services)
        {
            var data = services.GetRequiredService<RideSharingDbContext>();

            if (data.VehicleTypes.Any())
            {
                return;
            }

            data.VehicleTypes.AddRange(new[]
            {
                new VehicleType { Type = "Sedan" },
                new VehicleType { Type = "Coupe" },
                new VehicleType { Type = "Sports car" },
                new VehicleType { Type = "Station Wagon" },
                new VehicleType { Type = "Hatchback" },
                new VehicleType { Type = "Convertible" },
                new VehicleType { Type = "SUV" },
                new VehicleType { Type = "Pickup Truck" },
                new VehicleType { Type = "Van" },
                new VehicleType { Type = "Bus" },
            });

            data.SaveChanges();
        }

        //private static void SeedAdministrator(IServiceProvider services)
        //{
        //    var userManager = services.GetRequiredService<UserManager<User>>();
        //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        //    Task
        //        .Run(async () =>
        //        {
        //            if (await roleManager.RoleExistsAsync(AdministratorRole))
        //            {
        //                return;
        //            }

        //            var role = new IdentityRole { Name = AdministratorRole };

        //            await roleManager.CreateAsync(role);

        //            const string adminEmail = "admin@rs.com";
        //            const string adminPassword = "admin123";

        //            var user = new User
        //            {
        //                Email = adminEmail,
        //                UserName = adminEmail,
        //                FirstName = "Admin",
        //                LastName = "Admin"
        //            };

        //            await userManager.CreateAsync(user, adminPassword);

        //            await userManager.AddToRoleAsync(user, role.Name);
        //        })
        //        .GetAwaiter()
        //        .GetResult();
        //}
    }
}
