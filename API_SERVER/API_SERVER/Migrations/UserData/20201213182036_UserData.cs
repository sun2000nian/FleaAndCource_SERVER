using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_SERVER.Migrations.UserData
{
    public partial class UserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDataDb",
                columns: table => new
                {
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    displayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    studentID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDataDb", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "courceObjectsDb",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creaTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    displayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sponsorID_FK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    receiverID_FK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courceObjectsDb", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_courceObjectsDb_UserDataDb_receiverID_FK",
                        column: x => x.receiverID_FK,
                        principalTable: "UserDataDb",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_courceObjectsDb_UserDataDb_sponsorID_FK",
                        column: x => x.sponsorID_FK,
                        principalTable: "UserDataDb",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fleaObjectsDb",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creaTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    displayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sponsorID_FK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    receiverID_FK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fleaObjectsDb", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_fleaObjectsDb_UserDataDb_receiverID_FK",
                        column: x => x.receiverID_FK,
                        principalTable: "UserDataDb",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fleaObjectsDb_UserDataDb_sponsorID_FK",
                        column: x => x.sponsorID_FK,
                        principalTable: "UserDataDb",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourceModelPersonalData",
                columns: table => new
                {
                    courceObjects_LikedorderID = table.Column<int>(type: "int", nullable: false),
                    likedUserIDuserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourceModelPersonalData", x => new { x.courceObjects_LikedorderID, x.likedUserIDuserID });
                    table.ForeignKey(
                        name: "FK_CourceModelPersonalData_courceObjectsDb_courceObjects_LikedorderID",
                        column: x => x.courceObjects_LikedorderID,
                        principalTable: "courceObjectsDb",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourceModelPersonalData_UserDataDb_likedUserIDuserID",
                        column: x => x.likedUserIDuserID,
                        principalTable: "UserDataDb",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FleaObjectModelPersonalData",
                columns: table => new
                {
                    fleaObjects_LikedorderID = table.Column<int>(type: "int", nullable: false),
                    likedUserIDuserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FleaObjectModelPersonalData", x => new { x.fleaObjects_LikedorderID, x.likedUserIDuserID });
                    table.ForeignKey(
                        name: "FK_FleaObjectModelPersonalData_fleaObjectsDb_fleaObjects_LikedorderID",
                        column: x => x.fleaObjects_LikedorderID,
                        principalTable: "fleaObjectsDb",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FleaObjectModelPersonalData_UserDataDb_likedUserIDuserID",
                        column: x => x.likedUserIDuserID,
                        principalTable: "UserDataDb",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourceModelPersonalData_likedUserIDuserID",
                table: "CourceModelPersonalData",
                column: "likedUserIDuserID");

            migrationBuilder.CreateIndex(
                name: "IX_courceObjectsDb_receiverID_FK",
                table: "courceObjectsDb",
                column: "receiverID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_courceObjectsDb_sponsorID_FK",
                table: "courceObjectsDb",
                column: "sponsorID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_FleaObjectModelPersonalData_likedUserIDuserID",
                table: "FleaObjectModelPersonalData",
                column: "likedUserIDuserID");

            migrationBuilder.CreateIndex(
                name: "IX_fleaObjectsDb_receiverID_FK",
                table: "fleaObjectsDb",
                column: "receiverID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_fleaObjectsDb_sponsorID_FK",
                table: "fleaObjectsDb",
                column: "sponsorID_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourceModelPersonalData");

            migrationBuilder.DropTable(
                name: "FleaObjectModelPersonalData");

            migrationBuilder.DropTable(
                name: "courceObjectsDb");

            migrationBuilder.DropTable(
                name: "fleaObjectsDb");

            migrationBuilder.DropTable(
                name: "UserDataDb");
        }
    }
}
