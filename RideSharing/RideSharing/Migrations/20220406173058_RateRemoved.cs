using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Migrations
{
    public partial class RateRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RateTrips");

            migrationBuilder.DropTable(
                name: "Rates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    baseRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RateTrips",
                columns: table => new
                {
                    RateId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateTrips", x => new { x.RateId, x.TripId });
                    table.ForeignKey(
                        name: "FK_RateTrips_Rates_RateId",
                        column: x => x.RateId,
                        principalTable: "Rates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RateTrips_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RateTrips_TripId",
                table: "RateTrips",
                column: "TripId");
        }
    }
}
