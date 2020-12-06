using Microsoft.EntityFrameworkCore.Migrations;

namespace API_SERVER.Migrations.ServerSideUserData
{
    public partial class ServerSide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_ServerSides");

            migrationBuilder.DropColumn(
                name: "displayName",
                table: "ServerSide_UserData");

            migrationBuilder.DropColumn(
                name: "email",
                table: "ServerSide_UserData");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "ServerSide_UserData");

            migrationBuilder.DropColumn(
                name: "name",
                table: "ServerSide_UserData");

            migrationBuilder.DropColumn(
                name: "phoneNum",
                table: "ServerSide_UserData");

            migrationBuilder.DropColumn(
                name: "qq",
                table: "ServerSide_UserData");

            migrationBuilder.DropColumn(
                name: "userType",
                table: "ServerSide_UserData");

            migrationBuilder.AddColumn<string>(
                name: "AvatarFileName",
                table: "ServerSide_UserData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFileName",
                table: "ServerSide_UserData");

            migrationBuilder.AddColumn<string>(
                name: "displayName",
                table: "ServerSide_UserData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "ServerSide_UserData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "ServerSide_UserData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "ServerSide_UserData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNum",
                table: "ServerSide_UserData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "qq",
                table: "ServerSide_UserData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userType",
                table: "ServerSide_UserData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "data_ServerSides",
                columns: table => new
                {
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AvatarFileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_ServerSides", x => x.userID);
                });
        }
    }
}
