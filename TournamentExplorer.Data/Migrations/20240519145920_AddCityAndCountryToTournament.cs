using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentExplorer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCityAndCountryToTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Tournament",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tournament",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Tournament",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Tournament",
                newName: "Description");
        }
    }
}
