using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConjugationAPI.Migrations.Conjugation
{
    /// <inheritdoc />
    public partial class FixAnswerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Infinitive",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "Mood",
                table: "answers");

            migrationBuilder.RenameColumn(
                name: "Person",
                table: "answers",
                newName: "AnswerText");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnswerText",
                table: "answers",
                newName: "Person");

            migrationBuilder.AddColumn<string>(
                name: "Infinitive",
                table: "answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mood",
                table: "answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
