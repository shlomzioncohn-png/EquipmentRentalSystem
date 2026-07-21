using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class renameTheculloms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Item",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BusinessName",
                table: "Businesses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Businesses",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Item",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Businesses",
                newName: "BusinessName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Businesses",
                newName: "BusinessId");
        }
    }
}
