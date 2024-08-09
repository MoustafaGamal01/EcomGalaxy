using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomGalaxy.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentDateProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatmentDate",
                table: "Payments",
                newName: "PaymentDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Payments",
                newName: "PatmentDate");
        }
    }
}
