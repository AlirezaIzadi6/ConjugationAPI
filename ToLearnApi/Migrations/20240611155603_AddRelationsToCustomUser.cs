using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToLearnApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsToCustomUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                schema: "flashcards",
                table: "units");

            migrationBuilder.DropColumn(
                name: "Creator",
                schema: "flashcards",
                table: "cards");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "flashcards",
                table: "units",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "conjugation",
                table: "questions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "conjugation",
                table: "profiles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "flashcards",
                table: "learnStatuses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "flashcards",
                table: "items",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "flashcards",
                table: "decks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "flashcards",
                table: "cards",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_units_UserId",
                schema: "flashcards",
                table: "units",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_UserId",
                schema: "conjugation",
                table: "questions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_profiles_UserId",
                schema: "conjugation",
                table: "profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_learnStatuses_UserId",
                schema: "flashcards",
                table: "learnStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_items_UserId",
                schema: "flashcards",
                table: "items",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_decks_UserId",
                schema: "flashcards",
                table: "decks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_cards_UserId",
                schema: "flashcards",
                table: "cards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_cards_users_UserId",
                schema: "flashcards",
                table: "cards",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_decks_users_UserId",
                schema: "flashcards",
                table: "decks",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_items_users_UserId",
                schema: "flashcards",
                table: "items",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_learnStatuses_users_UserId",
                schema: "flashcards",
                table: "learnStatuses",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_profiles_users_UserId",
                schema: "conjugation",
                table: "profiles",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_questions_users_UserId",
                schema: "conjugation",
                table: "questions",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_units_users_UserId",
                schema: "flashcards",
                table: "units",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_users_UserId",
                schema: "flashcards",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_decks_users_UserId",
                schema: "flashcards",
                table: "decks");

            migrationBuilder.DropForeignKey(
                name: "FK_items_users_UserId",
                schema: "flashcards",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "FK_learnStatuses_users_UserId",
                schema: "flashcards",
                table: "learnStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_profiles_users_UserId",
                schema: "conjugation",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_questions_users_UserId",
                schema: "conjugation",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "FK_units_users_UserId",
                schema: "flashcards",
                table: "units");

            migrationBuilder.DropIndex(
                name: "IX_units_UserId",
                schema: "flashcards",
                table: "units");

            migrationBuilder.DropIndex(
                name: "IX_questions_UserId",
                schema: "conjugation",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_profiles_UserId",
                schema: "conjugation",
                table: "profiles");

            migrationBuilder.DropIndex(
                name: "IX_learnStatuses_UserId",
                schema: "flashcards",
                table: "learnStatuses");

            migrationBuilder.DropIndex(
                name: "IX_items_UserId",
                schema: "flashcards",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_decks_UserId",
                schema: "flashcards",
                table: "decks");

            migrationBuilder.DropIndex(
                name: "IX_cards_UserId",
                schema: "flashcards",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "flashcards",
                table: "units");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "flashcards",
                table: "decks");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "flashcards",
                table: "cards");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                schema: "flashcards",
                table: "units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "conjugation",
                table: "questions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "conjugation",
                table: "profiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "flashcards",
                table: "learnStatuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "flashcards",
                table: "items",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                schema: "flashcards",
                table: "cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
