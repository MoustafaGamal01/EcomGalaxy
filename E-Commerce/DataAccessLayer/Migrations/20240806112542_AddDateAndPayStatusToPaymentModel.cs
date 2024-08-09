using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomGalaxy.Migrations
{
    /// <inheritdoc />
    public partial class AddDateAndPayStatusToPaymentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PatmentDate",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatmentDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");
        }
    }
}
