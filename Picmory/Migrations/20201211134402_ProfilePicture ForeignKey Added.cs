using Microsoft.EntityFrameworkCore.Migrations;

namespace Picmory.Migrations
{
    public partial class ProfilePictureForeignKeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ProfPicture",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfPicture",
                table: "Users",
                column: "ProfPicture");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Pictures_ProfPicture",
                table: "Users",
                column: "ProfPicture",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Pictures_ProfPicture",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfPicture",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfPicture",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
