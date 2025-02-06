using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class CartModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItemsModels_itemsModels_itemsModelId",
                table: "cartItemsModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartItemsModels",
                table: "cartItemsModels");

            migrationBuilder.RenameTable(
                name: "cartItemsModels",
                newName: "cartItemsModel");

            migrationBuilder.RenameIndex(
                name: "IX_cartItemsModels_itemsModelId",
                table: "cartItemsModel",
                newName: "IX_cartItemsModel_itemsModelId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "cartItemsModel",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartItemsModel",
                table: "cartItemsModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItemsModel_itemsModels_itemsModelId",
                table: "cartItemsModel",
                column: "itemsModelId",
                principalTable: "itemsModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItemsModel_itemsModels_itemsModelId",
                table: "cartItemsModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartItemsModel",
                table: "cartItemsModel");

            migrationBuilder.RenameTable(
                name: "cartItemsModel",
                newName: "cartItemsModels");

            migrationBuilder.RenameIndex(
                name: "IX_cartItemsModel_itemsModelId",
                table: "cartItemsModels",
                newName: "IX_cartItemsModels_itemsModelId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "cartItemsModels",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartItemsModels",
                table: "cartItemsModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItemsModels_itemsModels_itemsModelId",
                table: "cartItemsModels",
                column: "itemsModelId",
                principalTable: "itemsModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
