using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ipman.core.Migrations
{
    public partial class SiteAccountColorAndType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SiteAccountThemeColorPrimary",
                table: "SiteAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteAccountThemeColorSecondary",
                table: "SiteAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteAccountType",
                table: "SiteAccounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SiteAccounts",
                keyColumn: "ID",
                keyValue: new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"),
                columns: new[] { "SiteAccountThemeColorPrimary", "SiteAccountThemeColorSecondary" },
                values: new object[] { "#01579b", "#8d6e63" });

            migrationBuilder.UpdateData(
                table: "SiteAccounts",
                keyColumn: "ID",
                keyValue: new Guid("6b93aaaa-8ef0-4d91-9574-fae60fc336a8"),
                columns: new[] { "SiteAccountThemeColorPrimary", "SiteAccountThemeColorSecondary", "SiteAccountType" },
                values: new object[] { "#004d40", "#bf360c", 1 });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteAccountThemeColorPrimary",
                table: "SiteAccounts");

            migrationBuilder.DropColumn(
                name: "SiteAccountThemeColorSecondary",
                table: "SiteAccounts");

            migrationBuilder.DropColumn(
                name: "SiteAccountType",
                table: "SiteAccounts");

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"),
                column: "UserAccountSalt",
                value: "3E8.3zQaTFAIB8Bq8TjR2BcR/Q==");

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"),
                column: "UserAccountSalt",
                value: "3E8.ii1IJat2/E/Ky2nBjsxKSA==");
        }
    }
}
