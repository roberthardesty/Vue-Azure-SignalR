using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ipman.core.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PostTitle = table.Column<string>(nullable: true),
                    PostDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SiteAccounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SiteAccountName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastUpdatedUTC = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAccounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TagName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    GitHubID = table.Column<string>(nullable: true),
                    GoogleID = table.Column<string>(nullable: true),
                    AvatarLink = table.Column<string>(nullable: true),
                    LastLoginProvider = table.Column<int>(nullable: false),
                    LastLoginUTC = table.Column<DateTime>(nullable: false),
                    CreatedUTC = table.Column<DateTime>(nullable: false),
                    LastUpdatedUTC = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DepartmentName = table.Column<string>(nullable: true),
                    CreatedUTC = table.Column<DateTime>(nullable: false),
                    LastUpdatedUTC = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    SiteAccountID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Departments_SiteAccounts_SiteAccountID",
                        column: x => x.SiteAccountID,
                        principalTable: "SiteAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostID = table.Column<Guid>(nullable: false),
                    TagID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostID, x.TagID });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteAccountUserAccount",
                columns: table => new
                {
                    SiteAccountID = table.Column<Guid>(nullable: false),
                    UserAccountID = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsMemberOfAllDepartments = table.Column<bool>(nullable: false),
                    LastLoginUTC = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAccountUserAccount", x => new { x.SiteAccountID, x.UserAccountID });
                    table.ForeignKey(
                        name: "FK_SiteAccountUserAccount_SiteAccounts_SiteAccountID",
                        column: x => x.SiteAccountID,
                        principalTable: "SiteAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteAccountUserAccount_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteAccountUserAccountDepartment",
                columns: table => new
                {
                    SiteAccountUserAccountID = table.Column<Guid>(nullable: false),
                    SiteAccountUserAccountSiteAccountID = table.Column<Guid>(nullable: true),
                    SiteAccountUserAccountUserAccountID = table.Column<Guid>(nullable: true),
                    DepartmentID = table.Column<Guid>(nullable: false),
                    SiteAccountID = table.Column<Guid>(nullable: true),
                    UserAccountID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAccountUserAccountDepartment", x => new { x.SiteAccountUserAccountID, x.DepartmentID });
                    table.ForeignKey(
                        name: "FK_SiteAccountUserAccountDepartment_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteAccountUserAccountDepartment_SiteAccounts_SiteAccountID",
                        column: x => x.SiteAccountID,
                        principalTable: "SiteAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteAccountUserAccountDepartment_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteAccountUserAccountDepartment_SiteAccountUserAccount_SiteAccountUserAccountSiteAccountID_SiteAccountUserAccountUserAccoun~",
                        columns: x => new { x.SiteAccountUserAccountSiteAccountID, x.SiteAccountUserAccountUserAccountID },
                        principalTable: "SiteAccountUserAccount",
                        principalColumns: new[] { "SiteAccountID", "UserAccountID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SiteAccountID",
                table: "Departments",
                column: "SiteAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagID",
                table: "PostTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAccountUserAccount_UserAccountID",
                table: "SiteAccountUserAccount",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAccountUserAccountDepartment_DepartmentID",
                table: "SiteAccountUserAccountDepartment",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAccountUserAccountDepartment_SiteAccountID",
                table: "SiteAccountUserAccountDepartment",
                column: "SiteAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAccountUserAccountDepartment_UserAccountID",
                table: "SiteAccountUserAccountDepartment",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAccountUserAccountDepartment_SiteAccountUserAccountSiteAccountID_SiteAccountUserAccountUserAccountID",
                table: "SiteAccountUserAccountDepartment",
                columns: new[] { "SiteAccountUserAccountSiteAccountID", "SiteAccountUserAccountUserAccountID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "SiteAccountUserAccountDepartment");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "SiteAccountUserAccount");

            migrationBuilder.DropTable(
                name: "SiteAccounts");

            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
