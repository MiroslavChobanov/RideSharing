using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Migrations
{
    public partial class VehiclesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Vehicles_VehicleId",
                table: "Trips");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Vehicles_VehicleId",
                table: "Trips",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Vehicles_VehicleId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Vehicles_VehicleId",
                table: "Trips",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
