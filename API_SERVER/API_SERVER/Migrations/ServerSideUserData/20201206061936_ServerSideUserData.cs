using Microsoft.EntityFrameworkCore.Migrations;

namespace API_SERVER.Migrations.ServerSideUserData
{
    public partial class ServerSideUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "data_ServerSides",
                columns: table => new
                {
                    userID = table.Column<string>(nullable: false),
                    AvatarFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_ServerSides", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "ServerSide_UserData",
                columns: table => new
                {
                    userID = table.Column<string>(nullable: false),
                    userType = table.Column<string>(nullable: true),
                    gender = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    displayName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    phoneNum = table.Column<string>(nullable: true),
                    qq = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerSide_UserData", x => x.userID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_ServerSides");

            migrationBuilder.DropTable(
                name: "ServerSide_UserData");
        }
    }
}
