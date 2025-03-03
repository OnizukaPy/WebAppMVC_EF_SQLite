using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMVC_EF_SQLite.Migrations
{
    /// <inheritdoc />
    public partial class AddMailToCostumer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Costumers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Costumers");
        }
    }
}
