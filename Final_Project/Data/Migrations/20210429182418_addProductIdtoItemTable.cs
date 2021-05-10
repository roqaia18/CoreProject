using Microsoft.EntityFrameworkCore.Migrations;

namespace Final_Project.Data.Migrations
{
    public partial class addProductIdtoItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Product_ProductID",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ProductID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "Product_Id",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Item_Product_Id",
                table: "Item",
                column: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Product_Product_Id",
                table: "Item",
                column: "Product_Id",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Product_Product_Id",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_Product_Id",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Product_Id",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ProductID",
                table: "Item",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Product_ProductID",
                table: "Item",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
