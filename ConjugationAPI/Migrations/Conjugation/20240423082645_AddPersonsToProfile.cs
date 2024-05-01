using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConjugationAPI.Migrations.Conjugation
{
    /// <inheritdoc />
    public partial class AddPersonsToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Persons",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Persons",
                table: "Profiles");
        }
    }
}
