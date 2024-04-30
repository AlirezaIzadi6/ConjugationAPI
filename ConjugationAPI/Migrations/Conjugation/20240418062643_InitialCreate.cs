using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConjugationAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moods = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Infinitives = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "conjugations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Infinitive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfinitiveEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoodEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tense = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenseEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerbEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Form1S = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form2S = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form3S = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form1P = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form2P = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form3P = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gerund = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GerundEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PastParticiple = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PastParticipleEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conjugations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "conjugations");
        }
    }
}
