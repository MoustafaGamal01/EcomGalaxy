using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomGalaxy.Migrations
{
    /// <inheritdoc />
    public partial class nullablePaymentDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Payments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ExpiryDate",
                table: "Payments",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CVV",
                table: "Payments",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            //migrationBuilder.AddColumn<int>(
            //    name: "ShoppingCartId",
            //    table: "Orders",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Orders_ShoppingCartId",
            //    table: "Orders",
            //    column: "ShoppingCartId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_ShoppingCarts_ShoppingCartId",
            //    table: "Orders",
            //    column: "ShoppingCartId",
            //    principalTable: "ShoppingCarts",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_ShoppingCarts_ShoppingCartId",
            //    table: "Orders");

            //migrationBuilder.DropIndex(
            //    name: "IX_Orders_ShoppingCartId",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "ShoppingCartId",
            //    table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Payments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpiryDate",
                table: "Payments",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CVV",
                table: "Payments",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);
        }
    }
}
