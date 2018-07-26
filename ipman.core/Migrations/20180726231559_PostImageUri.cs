using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ipman.core.Migrations
{
    public partial class PostImageUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostImageUri",
                table: "Posts",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"),
                column: "UserAccountSalt",
                value: "3E8.iL/PLb0fFSQrrmDzgCG+pA==");

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"),
                column: "UserAccountSalt",
                value: "3E8.MZRLST4gFUc+Ib0vXrwR0Q==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostImageUri",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"),
                column: "UserAccountSalt",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"),
                column: "UserAccountSalt",
                value: null);
        }
    }
}
