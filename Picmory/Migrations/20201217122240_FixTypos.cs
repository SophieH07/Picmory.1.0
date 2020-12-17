using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Picmory.Migrations
{
    public partial class FixTypos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EMail",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ColorTow",
                table: "Users",
                newName: "ColorTwo");

            migrationBuilder.AddColumn<byte[]>(
                name: "UploadDate",
                table: "Pictures",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "EMail");

            migrationBuilder.RenameColumn(
                name: "ColorTwo",
                table: "Users",
                newName: "ColorTow");
        }
    }
}
