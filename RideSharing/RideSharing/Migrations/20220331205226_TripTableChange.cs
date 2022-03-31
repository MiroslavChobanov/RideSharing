using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Migrations
{
    public partial class TripTableChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Comments_CommentId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_CommentId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Trips");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CommentId",
                table: "Trips",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Comments_CommentId",
                table: "Trips",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
