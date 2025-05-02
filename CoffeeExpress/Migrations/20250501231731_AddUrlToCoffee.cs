using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeExpress.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlToCoffee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Coffees",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Coffees");
        }
    }
}
