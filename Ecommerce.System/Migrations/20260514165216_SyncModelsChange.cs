using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.System.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelsChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_OrderId",
                table: "OrderStatuses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatuses_Orders_OrderId",
                table: "OrderStatuses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatuses_Orders_OrderId",
                table: "OrderStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatuses_OrderId",
                table: "OrderStatuses");
        }
    }
}
