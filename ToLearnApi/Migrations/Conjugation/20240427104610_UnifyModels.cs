using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToLearnApi.Migrations.Conjugation
{
    /// <inheritdoc />
    public partial class UnifyModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Profiles",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Profiles",
                newName: "ProfileId");
        }
    }
}
