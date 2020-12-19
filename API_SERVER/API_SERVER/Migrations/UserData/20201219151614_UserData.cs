﻿using System;
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
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isAccepted = table.Column<bool>(type: "bit", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    displayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sponsor = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    receiver = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    isClosed = table.Column<bool>(type: "bit", nullable: false),
                    closeTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courceObjectsDb", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_courceObjectsDb_UserDataDb_receiver",
                        column: x => x.receiver,
                        principalTable: "UserDataDb",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_courceObjectsDb_UserDataDb_sponsor",
                        column: x => x.sponsor,
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
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    displayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sponsor = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    receiver = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    isClosed = table.Column<bool>(type: "bit", nullable: false),
                    closeTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fleaObjectsDb", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_fleaObjectsDb_UserDataDb_receiver",
                        column: x => x.receiver,
                        principalTable: "UserDataDb",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fleaObjectsDb_UserDataDb_sponsor",
                        column: x => x.sponsor,
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

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FleaObjectModelorderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.id);
                    table.ForeignKey(
                        name: "FK_Picture_fleaObjectsDb_FleaObjectModelorderID",
                        column: x => x.FleaObjectModelorderID,
                        principalTable: "fleaObjectsDb",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourceModelPersonalData_likedUserIDuserID",
                table: "CourceModelPersonalData",
                column: "likedUserIDuserID");

            migrationBuilder.CreateIndex(
                name: "IX_courceObjectsDb_receiver",
                table: "courceObjectsDb",
                column: "receiver");

            migrationBuilder.CreateIndex(
                name: "IX_courceObjectsDb_sponsor",
                table: "courceObjectsDb",
                column: "sponsor");

            migrationBuilder.CreateIndex(
                name: "IX_FleaObjectModelPersonalData_likedUserIDuserID",
                table: "FleaObjectModelPersonalData",
                column: "likedUserIDuserID");

            migrationBuilder.CreateIndex(
                name: "IX_fleaObjectsDb_receiver",
                table: "fleaObjectsDb",
                column: "receiver");

            migrationBuilder.CreateIndex(
                name: "IX_fleaObjectsDb_sponsor",
                table: "fleaObjectsDb",
                column: "sponsor");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_FleaObjectModelorderID",
                table: "Picture",
                column: "FleaObjectModelorderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourceModelPersonalData");

            migrationBuilder.DropTable(
                name: "FleaObjectModelPersonalData");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "courceObjectsDb");

            migrationBuilder.DropTable(
                name: "fleaObjectsDb");

            migrationBuilder.DropTable(
                name: "UserDataDb");
        }
    }
}