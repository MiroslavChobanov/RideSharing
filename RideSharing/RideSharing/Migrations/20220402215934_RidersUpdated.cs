using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Migrations
{
    public partial class RidersUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LuggageSize",
                table: "Riders",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Riders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Riders");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Riders",
                newName: "LuggageSize");
        }
    }
}
