using Microsoft.EntityFrameworkCore.Migrations;

namespace Sandogh.Persistance.Migrations
{
    public partial class renameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adminMenus_adminMenus_AdminMenuId",
                table: "adminMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_bankAccounts_People_PersonID",
                table: "bankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankProfits_bankAccounts_BankAccountID",
                table: "BankProfits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bankAccounts",
                table: "bankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_adminMenus",
                table: "adminMenus");

            migrationBuilder.RenameTable(
                name: "bankAccounts",
                newName: "BankAccounts");

            migrationBuilder.RenameTable(
                name: "adminMenus",
                newName: "AdminMenus");

            migrationBuilder.RenameIndex(
                name: "IX_bankAccounts_PersonID",
                table: "BankAccounts",
                newName: "IX_BankAccounts_PersonID");

            migrationBuilder.RenameIndex(
                name: "IX_adminMenus_AdminMenuId",
                table: "AdminMenus",
                newName: "IX_AdminMenus_AdminMenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminMenus",
                table: "AdminMenus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminMenus_AdminMenus_AdminMenuId",
                table: "AdminMenus",
                column: "AdminMenuId",
                principalTable: "AdminMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_People_PersonID",
                table: "BankAccounts",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankProfits_BankAccounts_BankAccountID",
                table: "BankProfits",
                column: "BankAccountID",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminMenus_AdminMenus_AdminMenuId",
                table: "AdminMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_People_PersonID",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankProfits_BankAccounts_BankAccountID",
                table: "BankProfits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminMenus",
                table: "AdminMenus");

            migrationBuilder.RenameTable(
                name: "BankAccounts",
                newName: "bankAccounts");

            migrationBuilder.RenameTable(
                name: "AdminMenus",
                newName: "adminMenus");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_PersonID",
                table: "bankAccounts",
                newName: "IX_bankAccounts_PersonID");

            migrationBuilder.RenameIndex(
                name: "IX_AdminMenus_AdminMenuId",
                table: "adminMenus",
                newName: "IX_adminMenus_AdminMenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bankAccounts",
                table: "bankAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_adminMenus",
                table: "adminMenus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_adminMenus_adminMenus_AdminMenuId",
                table: "adminMenus",
                column: "AdminMenuId",
                principalTable: "adminMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bankAccounts_People_PersonID",
                table: "bankAccounts",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankProfits_bankAccounts_BankAccountID",
                table: "BankProfits",
                column: "BankAccountID",
                principalTable: "bankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
