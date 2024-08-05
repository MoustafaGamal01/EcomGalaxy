using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomGalaxy.Migrations
{
    /// <inheritdoc />
    public partial class AddedSellerproptoOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Orders",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                newName: "IX_Orders_SellerId");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_SellerId",
                table: "Orders",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_SellerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Orders",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_SellerId",
                table: "Orders",
                newName: "IX_Orders_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
