using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductEntityTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Repaired",
                table: "ProductVariations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repaired",
                table: "ProductVariations");
        }
    }
}
