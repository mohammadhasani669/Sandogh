using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sandogh.Persistance.Migrations
{
    public partial class Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "People",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "OperationsLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "OperationsLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "Loans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Loans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "LoanRepayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "LoanRepayments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "Emails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Emails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "BlackLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "BlackLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "BankProfits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "BankProfits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "bankAccounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "bankAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "bankAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "People");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "OperationsLogs");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "OperationsLogs");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "LoanRepayments");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "LoanRepayments");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "BlackLists");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "BlackLists");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "BankProfits");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "BankProfits");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "bankAccounts");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "bankAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "bankAccounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
