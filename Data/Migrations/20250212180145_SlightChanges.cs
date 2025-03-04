using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class SlightChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_itemsModels",
                table: "itemsModels");

            migrationBuilder.RenameTable(
                name: "itemsModels",
                newName: "ItemsModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsModel",
                table: "ItemsModel",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsModel",
                table: "ItemsModel");

            migrationBuilder.RenameTable(
                name: "ItemsModel",
                newName: "itemsModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itemsModels",
                table: "itemsModels",
                column: "Id");
        }
    }
}
