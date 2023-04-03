using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Migrations
{
    /// <inheritdoc />
    public partial class AddLapNumberToRaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LapNumber",
                table: "Races",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LapNumber",
                table: "Races");
        }
    }
}
