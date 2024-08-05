using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomGalaxy.Migrations
{
    /// <inheritdoc />
    public partial class AddNameOnCardPaymentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "payments",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "payments",
                newName: "PaymentMethod");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "payments",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
