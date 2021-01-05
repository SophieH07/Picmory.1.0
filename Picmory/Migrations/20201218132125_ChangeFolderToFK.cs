using Microsoft.EntityFrameworkCore.Migrations;

namespace Picmory.Migrations
{
    public partial class ChangeFolderToFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderName",
                table: "Pictures");

            migrationBuilder.AddColumn<int>(
                name: "FolderData",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_FolderData",
                table: "Pictures",
                column: "FolderData");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Folders_FolderData",
                table: "Pictures",
                column: "FolderData",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Folders_FolderData",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_FolderData",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "FolderData",
                table: "Pictures");

            migrationBuilder.AddColumn<string>(
                name: "FolderName",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
