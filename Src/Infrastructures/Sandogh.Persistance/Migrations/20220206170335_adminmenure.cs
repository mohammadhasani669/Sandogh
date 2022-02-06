using Microsoft.EntityFrameworkCore.Migrations;

namespace Sandogh.Persistance.Migrations
{
    public partial class adminmenure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminMenus_AdminMenus_AdminMenuId",
                table: "AdminMenus");

            migrationBuilder.DropIndex(
                name: "IX_AdminMenus_AdminMenuId",
                table: "AdminMenus");

            migrationBuilder.DropColumn(
                name: "AdminMenuId",
                table: "AdminMenus");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMenus_ParentId",
                table: "AdminMenus",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminMenus_AdminMenus_ParentId",
                table: "AdminMenus",
                column: "ParentId",
                principalTable: "AdminMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminMenus_AdminMenus_ParentId",
                table: "AdminMenus");

            migrationBuilder.DropIndex(
                name: "IX_AdminMenus_ParentId",
                table: "AdminMenus");

            migrationBuilder.AddColumn<int>(
                name: "AdminMenuId",
                table: "AdminMenus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminMenus_AdminMenuId",
                table: "AdminMenus",
                column: "AdminMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminMenus_AdminMenus_AdminMenuId",
                table: "AdminMenus",
                column: "AdminMenuId",
                principalTable: "AdminMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
