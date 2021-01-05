using Microsoft.EntityFrameworkCore.Migrations;

namespace Picmory.Migrations
{
    public partial class PictureUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_PictureOwner",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_PictureOwner",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "PictureOwner",
                table: "Folders");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "FolderOwner",
                table: "Folders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FolderOwner",
                table: "Folders",
                column: "FolderOwner");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_FolderOwner",
                table: "Folders",
                column: "FolderOwner",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_FolderOwner",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_FolderOwner",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FolderOwner",
                table: "Folders");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureOwner",
                table: "Folders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Folders_PictureOwner",
                table: "Folders",
                column: "PictureOwner");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_PictureOwner",
                table: "Folders",
                column: "PictureOwner",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
