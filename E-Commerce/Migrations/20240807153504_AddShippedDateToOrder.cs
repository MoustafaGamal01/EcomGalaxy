using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomGalaxy.Migrations
{
    /// <inheritdoc />
    public partial class AddShippedDateToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Orders",
                newName: "ShippedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderedDate",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderedDate",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShippedDate",
                table: "Orders",
                newName: "Date");
        }
    }
}
