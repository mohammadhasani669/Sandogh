﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Sandogh.Persistance.Migrations
{
    public partial class RenameFiled2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductBrandId",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_BrandId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "ProductBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                newName: "IX_Products_ProductBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId",
                table: "Products",
                column: "ProductBrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
