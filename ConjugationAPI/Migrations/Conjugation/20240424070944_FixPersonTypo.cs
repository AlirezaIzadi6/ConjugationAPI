using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConjugationAPI.Migrations.Conjugation
{
    /// <inheritdoc />
    public partial class FixPersonTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "person",
                table: "questions",
                newName: "Person");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Person",
                table: "questions",
                newName: "person");
        }
    }
}
