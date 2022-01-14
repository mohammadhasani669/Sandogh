using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sandogh.Persistance.Migrations
{
    public partial class AdminMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adminMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    AdminMenuId = table.Column<int>(type: "int", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adminMenus_adminMenus_AdminMenuId",
                        column: x => x.AdminMenuId,
                        principalTable: "adminMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adminMenus_AdminMenuId",
                table: "adminMenus",
                column: "AdminMenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adminMenus");
        }
    }
}
