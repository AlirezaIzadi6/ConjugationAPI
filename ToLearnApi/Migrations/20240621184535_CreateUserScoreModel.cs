using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToLearnApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserScoreModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_users_UserId",
                schema: "flashcards",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_items_users_UserId",
                schema: "flashcards",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "FK_learnStatuses_users_UserId",
                schema: "flashcards",
                table: "learnStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_units_users_UserId",
                schema: "flashcards",
                table: "units");

            migrationBuilder.CreateTable(
                name: "UserScores",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserScores_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserScores_UserId",
                schema: "security",
                table: "UserScores",
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
                name: "FK_items_users_UserId",
                schema: "flashcards",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "FK_learnStatuses_users_UserId",
                schema: "flashcards",
                table: "learnStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_units_users_UserId",
                schema: "flashcards",
                table: "units");

            migrationBuilder.DropTable(
                name: "UserScores",
                schema: "security");

            migrationBuilder.AddForeignKey(
                name: "FK_cards_users_UserId",
                schema: "flashcards",
                table: "cards",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_items_users_UserId",
                schema: "flashcards",
                table: "items",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_learnStatuses_users_UserId",
                schema: "flashcards",
                table: "learnStatuses",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_units_users_UserId",
                schema: "flashcards",
                table: "units",
                column: "UserId",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
