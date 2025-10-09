using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITP_Assignment.Migrations
{
    /// <inheritdoc />
    public partial class jhd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UerId",
                table: "Users",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "UerId");
        }
    }
}
