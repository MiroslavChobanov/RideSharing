using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSharing.Migrations
{
    public partial class CommentsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Trips_TripId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Comments",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEditedOn",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Trips_TripId",
                table: "Comments",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Trips_TripId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastEditedOn",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Trips_TripId",
                table: "Comments",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");
        }
    }
}
