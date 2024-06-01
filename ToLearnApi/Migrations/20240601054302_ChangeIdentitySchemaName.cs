using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToLearnApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdentitySchemaName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.RenameTable(
                name: "userTokens",
                schema: "identity",
                newName: "userTokens",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "identity",
                newName: "users",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "userRoles",
                schema: "identity",
                newName: "userRoles",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "userLogins",
                schema: "identity",
                newName: "userLogins",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "userClaims",
                schema: "identity",
                newName: "userClaims",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "identity",
                newName: "roles",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "roleClaims",
                schema: "identity",
                newName: "roleClaims",
                newSchema: "security");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.RenameTable(
                name: "userTokens",
                schema: "security",
                newName: "userTokens",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "security",
                newName: "users",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "userRoles",
                schema: "security",
                newName: "userRoles",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "userLogins",
                schema: "security",
                newName: "userLogins",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "userClaims",
                schema: "security",
                newName: "userClaims",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "security",
                newName: "roles",
                newSchema: "identity");

            migrationBuilder.RenameTable(
                name: "roleClaims",
                schema: "security",
                newName: "roleClaims",
                newSchema: "identity");
        }
    }
}
