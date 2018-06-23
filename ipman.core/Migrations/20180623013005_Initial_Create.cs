using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ipman.core.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteAccounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SiteAccountName = table.Column<string>(nullable: false),
                    SiteAccountImagePath = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedUTC = table.Column<DateTime>(nullable: false),
                    LastUpdatedUTC = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAccounts", x => x.ID);
                    table.UniqueConstraint("AK_SiteAccounts_SiteAccountName", x => x.SiteAccountName);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    GitHubID = table.Column<string>(nullable: true),
                    GoogleID = table.Column<string>(nullable: true),
                    AvatarLink = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
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
                    SiteAccountID = table.Column<Guid>(nullable: false)
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
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TagName = table.Column<string>(nullable: true),
                    TagImage = table.Column<string>(nullable: true),
                    SiteAccountID = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedUTC = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tags_SiteAccounts_SiteAccountID",
                        column: x => x.SiteAccountID,
                        principalTable: "SiteAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PostTitle = table.Column<string>(nullable: true),
                    PostDescription = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    StartTimeUTC = table.Column<DateTime>(nullable: true),
                    LockTimeUTC = table.Column<DateTime>(nullable: true),
                    EndTimeUTC = table.Column<DateTime>(nullable: true),
                    CreatedUTC = table.Column<DateTime>(nullable: false),
                    LastUpdatedUTC = table.Column<DateTime>(nullable: false),
                    SiteAccountID = table.Column<Guid>(nullable: false),
                    UserAccountCreatorID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Posts_SiteAccounts_SiteAccountID",
                        column: x => x.SiteAccountID,
                        principalTable: "SiteAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_UserAccounts_UserAccountCreatorID",
                        column: x => x.UserAccountCreatorID,
                        principalTable: "UserAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    CreatedUTC = table.Column<DateTime>(nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteAccountUserAccount_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wager",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserAccountID = table.Column<Guid>(nullable: false),
                    WagerAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wager", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wager_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostChoice",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ChoiceName = table.Column<string>(nullable: true),
                    ChoiceValue = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PostID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostChoice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostChoice_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostID = table.Column<Guid>(nullable: false),
                    TagID = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedUTC = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostID, x.TagID });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PostID = table.Column<Guid>(nullable: false),
                    UserAccountID = table.Column<Guid>(nullable: false),
                    CreatedUTC = table.Column<DateTime>(nullable: false),
                    IsUpTally = table.Column<bool>(nullable: false),
                    IsComment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Votes_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteAccountUserAccountDepartment",
                columns: table => new
                {
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedUTC = table.Column<DateTime>(nullable: false),
                    SiteAccountUserAccountID = table.Column<Guid>(nullable: false),
                    SiteAccountUserSiteAccountID = table.Column<Guid>(nullable: true),
                    SiteAccountUserUserAccountID = table.Column<Guid>(nullable: true),
                    DepartmentID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAccountUserAccountDepartment", x => new { x.SiteAccountUserAccountID, x.DepartmentID });
                    table.ForeignKey(
                        name: "FK_SiteAccountUserAccountDepartment_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteAccountUserAccountDepartment_SiteAccountUserAccount_SiteAccountUserSiteAccountID_SiteAccountUserUserAccountID",
                        columns: x => new { x.SiteAccountUserSiteAccountID, x.SiteAccountUserUserAccountID },
                        principalTable: "SiteAccountUserAccount",
                        principalColumns: new[] { "SiteAccountID", "UserAccountID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostWager",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PostID = table.Column<Guid>(nullable: false),
                    WagerID = table.Column<Guid>(nullable: false),
                    Prediction = table.Column<int>(nullable: false),
                    RangeLength = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostWager", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostWager_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostWager_Wager_WagerID",
                        column: x => x.WagerID,
                        principalTable: "Wager",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "SiteAccounts",
                columns: new[] { "ID", "CreatedUTC", "IsActive", "LastUpdatedUTC", "SiteAccountImagePath", "SiteAccountName" },
                values: new object[] { new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Awesome Possum Admins" });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "ID", "AvatarLink", "CreatedUTC", "EmailAddress", "FirstName", "GitHubID", "GoogleID", "IsActive", "LastLoginProvider", "LastLoginUTC", "LastName", "LastUpdatedUTC", "Username" },
                values: new object[] { new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"), "https://lh4.googleusercontent.com/-gPvw9sU8Mpc/AAAAAAAAAAI/AAAAAAAAAOY/HQ2yjXFKeEk/photo.jpg?sz=50", new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "budnjoe@gmail.com", "rob", null, null, true, 0, new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hardesty", new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trundle" });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "ID", "AvatarLink", "CreatedUTC", "EmailAddress", "FirstName", "GitHubID", "GoogleID", "IsActive", "LastLoginProvider", "LastLoginUTC", "LastName", "LastUpdatedUTC", "Username" },
                values: new object[] { new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"), "https://lh3.googleusercontent.com/-pu8oCttY3pE/AAAAAAAAAAI/AAAAAAAAAAA/h5YVW6XWCK4/photo.jpg?sz=50", new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "robert.hardesty.mail@gmail.com", "Robert", null, null, true, 0, new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hardesty", new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "DreadPirateRobert" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "CreatedUTC", "DepartmentName", "IsActive", "LastUpdatedUTC", "SiteAccountID" },
                values: new object[] { new Guid("e29241de-853c-4a05-9928-1c6ac7f73b25"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "I AM THE WALRUS", true, new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8") });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "ID", "CreatedUTC", "EndTimeUTC", "IsActive", "IsLocked", "LastUpdatedUTC", "LockTimeUTC", "PostDescription", "PostTitle", "SiteAccountID", "StartTimeUTC", "UserAccountCreatorID" },
                values: new object[,]
                {
                    { new Guid("8c84658a-2210-4f5d-a7e2-7da2b32e64c3"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "How else could this all have been possible?", "I BET: Rob is 80% Wizard", new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), null, new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30") },
                    { new Guid("f723bae5-b6c0-43e0-8828-82adb3e9b088"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, new DateTime(2018, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "How many kills will mango average on a daily basis for the month of may?", "Average Kills Per Day In The Month May for MANG0", new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), null, new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30") },
                    { new Guid("7e6333c5-8223-4ff4-a3f5-27e97fa087a6"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, new DateTime(2018, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Lawyers were not obtained and therefore this endeavor has led to all of us going directly to jail. See you there! Also if you know any lawyers please send me their contact info.", "I BET: All Admins Go Directly To Jail, Do Not Pass GO. Do Not Collect 200 Dollars", new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), null, new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43") }
                });

            migrationBuilder.InsertData(
                table: "SiteAccountUserAccount",
                columns: new[] { "SiteAccountID", "UserAccountID", "CreatedUTC", "IsActive", "IsMemberOfAllDepartments", "LastLoginUTC", "RoleID" },
                values: new object[,]
                {
                    { new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2fa6b838-8b5b-4bb0-b888-ffce4fb7b7b7") },
                    { new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2fa6b838-8b5b-4bb0-b888-ffce4fb7b7b7") }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "ID", "CreatedUTC", "IsActive", "SiteAccountID", "TagImage", "TagName" },
                values: new object[,]
                {
                    { new Guid("afd3afe9-f67a-47e4-81f2-91efa7a2fccd"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), "admins.svg", "Admin" },
                    { new Guid("221e7112-b974-4269-b3c2-1f9a0c5e8fff"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), "user.svg", "BasedOnUser" },
                    { new Guid("1a24966b-2979-49f9-8cc4-b21b6449f53e"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), "admins.svg", "PickANumber" },
                    { new Guid("09965041-0340-4fc7-82c4-0e65811c2ca2"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), "admins.svg", "TrueFalse" }
                });

            migrationBuilder.InsertData(
                table: "PostTag",
                columns: new[] { "PostID", "TagID", "CreatedUTC", "IsActive" },
                values: new object[,]
                {
                    { new Guid("8c84658a-2210-4f5d-a7e2-7da2b32e64c3"), new Guid("09965041-0340-4fc7-82c4-0e65811c2ca2"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("8c84658a-2210-4f5d-a7e2-7da2b32e64c3"), new Guid("221e7112-b974-4269-b3c2-1f9a0c5e8fff"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("f723bae5-b6c0-43e0-8828-82adb3e9b088"), new Guid("1a24966b-2979-49f9-8cc4-b21b6449f53e"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("f723bae5-b6c0-43e0-8828-82adb3e9b088"), new Guid("221e7112-b974-4269-b3c2-1f9a0c5e8fff"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { new Guid("7e6333c5-8223-4ff4-a3f5-27e97fa087a6"), new Guid("afd3afe9-f67a-47e4-81f2-91efa7a2fccd"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false }
                });

            migrationBuilder.InsertData(
                table: "SiteAccountUserAccountDepartment",
                columns: new[] { "SiteAccountUserAccountID", "DepartmentID", "CreatedUTC", "IsActive", "SiteAccountUserSiteAccountID", "SiteAccountUserUserAccountID" },
                values: new object[,]
                {
                    { new Guid("143bf075-478f-4e1a-b8d8-889be2af42a4"), new Guid("e29241de-853c-4a05-9928-1c6ac7f73b25"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null },
                    { new Guid("f9c6aae5-fe6c-43bf-92b4-bf79ba92d2fe"), new Guid("e29241de-853c-4a05-9928-1c6ac7f73b25"), new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SiteAccountID",
                table: "Departments",
                column: "SiteAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PostChoice_PostID",
                table: "PostChoice",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SiteAccountID",
                table: "Posts",
                column: "SiteAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserAccountCreatorID",
                table: "Posts",
                column: "UserAccountCreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagID",
                table: "PostTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_PostWager_PostID",
                table: "PostWager",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostWager_WagerID",
                table: "PostWager",
                column: "WagerID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAccountUserAccount_UserAccountID",
                table: "SiteAccountUserAccount",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAccountUserAccountDepartment_DepartmentID",
                table: "SiteAccountUserAccountDepartment",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAccountUserAccountDepartment_SiteAccountUserSiteAccountID_SiteAccountUserUserAccountID",
                table: "SiteAccountUserAccountDepartment",
                columns: new[] { "SiteAccountUserSiteAccountID", "SiteAccountUserUserAccountID" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SiteAccountID",
                table: "Tags",
                column: "SiteAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PostID",
                table: "Votes",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserAccountID",
                table: "Votes",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Wager_UserAccountID",
                table: "Wager",
                column: "UserAccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostChoice");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "PostWager");

            migrationBuilder.DropTable(
                name: "SiteAccountUserAccountDepartment");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Wager");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "SiteAccountUserAccount");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "SiteAccounts");

            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
