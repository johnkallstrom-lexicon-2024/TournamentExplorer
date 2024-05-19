using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentExplorer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMorePropertiesToTournamentAndGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Game",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tournament",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Tournament",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Tournament",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Game");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Game",
                newName: "Title");
        }
    }
}
