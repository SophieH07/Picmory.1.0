using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Picmory.Migrations
{
    public partial class PictureUploadDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Pictures");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Pictures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Pictures",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Pictures");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Pictures",
                type: "datetime2",
                rowVersion: true,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
