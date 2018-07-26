using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ipman.core.Migrations
{
    public partial class RobsRaspSiteSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SiteAccounts",
                columns: new[] { "ID", "CreatedUTC", "IsActive", "LastUpdatedUTC", "SiteAccountImagePath", "SiteAccountName" },
                values: new object[] { new Guid("6b93aaaa-8ef0-4d91-9574-fae60fc336a8"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rob's Raspberries" });

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

            migrationBuilder.InsertData(
                table: "SiteAccountUserAccount",
                columns: new[] { "SiteAccountID", "UserAccountID", "CreatedUTC", "IsActive", "IsMemberOfAllDepartments", "LastLoginUTC", "RoleID" },
                values: new object[] { new Guid("6b93aaaa-8ef0-4d91-9574-fae60fc336a8"), new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2fa6b838-8b5b-4bb0-b888-ffce4fb7b7b7") });

            migrationBuilder.InsertData(
                table: "SiteAccountUserAccount",
                columns: new[] { "SiteAccountID", "UserAccountID", "CreatedUTC", "IsActive", "IsMemberOfAllDepartments", "LastLoginUTC", "RoleID" },
                values: new object[] { new Guid("6b93aaaa-8ef0-4d91-9574-fae60fc336a8"), new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2fa6b838-8b5b-4bb0-b888-ffce4fb7b7b7") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteAccountUserAccount",
                keyColumns: new[] { "SiteAccountID", "UserAccountID" },
                keyValues: new object[] { new Guid("6b93aaaa-8ef0-4d91-9574-fae60fc336a8"), new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30") });

            migrationBuilder.DeleteData(
                table: "SiteAccountUserAccount",
                keyColumns: new[] { "SiteAccountID", "UserAccountID" },
                keyValues: new object[] { new Guid("6b93aaaa-8ef0-4d91-9574-fae60fc336a8"), new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43") });

            migrationBuilder.DeleteData(
                table: "SiteAccounts",
                keyColumn: "ID",
                keyValue: new Guid("6b93aaaa-8ef0-4d91-9574-fae60fc336a8"));

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
    }
}
