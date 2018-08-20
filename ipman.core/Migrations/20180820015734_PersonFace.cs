using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ipman.core.Migrations
{
    public partial class PersonFace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostWager");

            migrationBuilder.DropTable(
                name: "Wager");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CreatedUTC = table.Column<DateTime>(nullable: false),
                    LastUpdatedUTC = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FaceCollection",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PersonID = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedUTC = table.Column<DateTime>(nullable: false),
                    LastUpdatedUTC = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceCollection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FaceCollection_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FaceImage",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    FaceCollectionID = table.Column<Guid>(nullable: false),
                    BlobPath = table.Column<string>(nullable: true),
                    CreatedUTC = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FaceImage_FaceCollection_FaceCollectionID",
                        column: x => x.FaceCollectionID,
                        principalTable: "FaceCollection",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"),
                column: "UserAccountSalt",
                value: "3E8.MyXnJrhEQRS+CzUZwqVSjw==");

            migrationBuilder.UpdateData(
                table: "UserAccounts",
                keyColumn: "ID",
                keyValue: new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"),
                column: "UserAccountSalt",
                value: "3E8.RMKykHNn/arMklPP5dD/Zw==");

            migrationBuilder.CreateIndex(
                name: "IX_FaceCollection_PersonID",
                table: "FaceCollection",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_FaceImage_FaceCollectionID",
                table: "FaceImage",
                column: "FaceCollectionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaceImage");

            migrationBuilder.DropTable(
                name: "FaceCollection");

            migrationBuilder.DropTable(
                name: "Persons");

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
                name: "PostWager",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PostID = table.Column<Guid>(nullable: false),
                    Prediction = table.Column<int>(nullable: false),
                    RangeLength = table.Column<int>(nullable: true),
                    WagerID = table.Column<Guid>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_PostWager_PostID",
                table: "PostWager",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostWager_WagerID",
                table: "PostWager",
                column: "WagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Wager_UserAccountID",
                table: "Wager",
                column: "UserAccountID");
        }
    }
}
