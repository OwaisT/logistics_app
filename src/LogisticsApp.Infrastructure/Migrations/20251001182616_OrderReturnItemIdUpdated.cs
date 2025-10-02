using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogisticsApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderReturnItemIdUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderReturnItem",
                table: "OrderReturnItem");

            migrationBuilder.DropColumn(
                name: "Id1",
                table: "OrderReturnItem");
            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderReturnItem");
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderReturnItem",
                type: "integer",
                nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderItemId",
                table: "OrderReturnItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderReturnItem",
                table: "OrderReturnItem",
                columns: new[] { "OrderReturnId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderReturnItem",
                table: "OrderReturnItem");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "OrderReturnItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "OrderReturnItem",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id1",
                table: "OrderReturnItem",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderReturnItem",
                table: "OrderReturnItem",
                columns: new[] { "OrderReturnId", "Id1" });
        }
    }
}
