using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConjugationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAnswerToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "answers");

            migrationBuilder.RenameColumn(
                name: "infinitive",
                table: "questions",
                newName: "Infinitive");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "questions");

            migrationBuilder.RenameColumn(
                name: "Infinitive",
                table: "questions",
                newName: "infinitive");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
