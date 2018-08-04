using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ipman.core.Migrations
{
    public partial class SiteAccountUserAccount_DbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteAccountUserAccount_SiteAccounts_SiteAccountID",
                table: "SiteAccountUserAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAccountUserAccount_UserAccounts_UserAccountID",
                table: "SiteAccountUserAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAccountUserAccountDepartment_SiteAccountUserAccount_SiteAccountUserSiteAccountID_SiteAccountUserUserAccountID",
                table: "SiteAccountUserAccountDepartment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteAccountUserAccount",
                table: "SiteAccountUserAccount");

            migrationBuilder.RenameTable(
                name: "SiteAccountUserAccount",
                newName: "SiteAccountUserAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_SiteAccountUserAccount_UserAccountID",
                table: "SiteAccountUserAccounts",
                newName: "IX_SiteAccountUserAccounts_UserAccountID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteAccountUserAccounts",
                table: "SiteAccountUserAccounts",
                columns: new[] { "SiteAccountID", "UserAccountID" });

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"),
                column: "UserAccountSalt",
                value: "3E8.TdXOCe1wK34QjxWiMzH1KQ==");

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"),
                column: "UserAccountSalt",
                value: "3E8.biCWyQp4IfsJf+3Cm8qLFg==");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAccountUserAccountDepartment_SiteAccountUserAccounts_SiteAccountUserSiteAccountID_SiteAccountUserUserAccountID",
                table: "SiteAccountUserAccountDepartment",
                columns: new[] { "SiteAccountUserSiteAccountID", "SiteAccountUserUserAccountID" },
                principalTable: "SiteAccountUserAccounts",
                principalColumns: new[] { "SiteAccountID", "UserAccountID" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAccountUserAccounts_SiteAccounts_SiteAccountID",
                table: "SiteAccountUserAccounts",
                column: "SiteAccountID",
                principalTable: "SiteAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAccountUserAccounts_UserAccounts_UserAccountID",
                table: "SiteAccountUserAccounts",
                column: "UserAccountID",
                principalTable: "UserAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteAccountUserAccountDepartment_SiteAccountUserAccounts_SiteAccountUserSiteAccountID_SiteAccountUserUserAccountID",
                table: "SiteAccountUserAccountDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAccountUserAccounts_SiteAccounts_SiteAccountID",
                table: "SiteAccountUserAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAccountUserAccounts_UserAccounts_UserAccountID",
                table: "SiteAccountUserAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteAccountUserAccounts",
                table: "SiteAccountUserAccounts");

            migrationBuilder.RenameTable(
                name: "SiteAccountUserAccounts",
                newName: "SiteAccountUserAccount");

            migrationBuilder.RenameIndex(
                name: "IX_SiteAccountUserAccounts_UserAccountID",
                table: "SiteAccountUserAccount",
                newName: "IX_SiteAccountUserAccount_UserAccountID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteAccountUserAccount",
                table: "SiteAccountUserAccount",
                columns: new[] { "SiteAccountID", "UserAccountID" });

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"),
                column: "UserAccountSalt",
                value: "3E8.A1/Jcbn6SlZ/pU0VfuF5Qw==");

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"),
                column: "UserAccountSalt",
                value: "3E8.e1vFNfCZYUDg4dAp4Fx26A==");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAccountUserAccount_SiteAccounts_SiteAccountID",
                table: "SiteAccountUserAccount",
                column: "SiteAccountID",
                principalTable: "SiteAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAccountUserAccount_UserAccounts_UserAccountID",
                table: "SiteAccountUserAccount",
                column: "UserAccountID",
                principalTable: "UserAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAccountUserAccountDepartment_SiteAccountUserAccount_SiteAccountUserSiteAccountID_SiteAccountUserUserAccountID",
                table: "SiteAccountUserAccountDepartment",
                columns: new[] { "SiteAccountUserSiteAccountID", "SiteAccountUserUserAccountID" },
                principalTable: "SiteAccountUserAccount",
                principalColumns: new[] { "SiteAccountID", "UserAccountID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
