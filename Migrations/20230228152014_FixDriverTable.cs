using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formula1.Migrations
{
    /// <inheritdoc />
    public partial class FixDriverTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Teams_TemaId",
                table: "Drivers");

            migrationBuilder.RenameColumn(
                name: "number",
                table: "Drivers",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "TemaId",
                table: "Drivers",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_TemaId",
                table: "Drivers",
                newName: "IX_Drivers_TeamId");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Drivers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Teams_TeamId",
                table: "Drivers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Teams_TeamId",
                table: "Drivers");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Drivers",
                newName: "number");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Drivers",
                newName: "TemaId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_TeamId",
                table: "Drivers",
                newName: "IX_Drivers_TemaId");

            migrationBuilder.AlterColumn<string>(
                name: "number",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Teams_TemaId",
                table: "Drivers",
                column: "TemaId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
