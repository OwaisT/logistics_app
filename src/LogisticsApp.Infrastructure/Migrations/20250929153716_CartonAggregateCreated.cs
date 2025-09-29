using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogisticsApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CartonAggregateCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cartons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: true),
                    WarehouseName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: true),
                    RoomName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OnLeft = table.Column<int>(type: "integer", nullable: true),
                    Below = table.Column<int>(type: "integer", nullable: true),
                    Behind = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartonItem",
                columns: table => new
                {
                    CartonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    VariationId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartonItem", x => new { x.CartonId, x.Id });
                    table.ForeignKey(
                        name: "FK_CartonItem_Cartons_CartonId",
                        column: x => x.CartonId,
                        principalTable: "Cartons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartons_WarehouseId_RoomId_OnLeft_Below_Behind",
                table: "Cartons",
                columns: new[] { "WarehouseId", "RoomId", "OnLeft", "Below", "Behind" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartonItem");

            migrationBuilder.DropTable(
                name: "Cartons");
        }
    }
}
