using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToLearnApi.Migrations.Conjugation
{
    /// <inheritdoc />
    public partial class AddCreatorPropertyForCardAndUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "units");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "cards");
        }
    }
}
