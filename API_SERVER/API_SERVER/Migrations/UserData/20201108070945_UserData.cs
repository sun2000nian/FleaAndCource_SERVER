using Microsoft.EntityFrameworkCore.Migrations;

namespace API_SERVER.Migrations.UserData
{
    public partial class UserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserData",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
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
                    table.PrimaryKey("PK_UserData", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserData");
        }
    }
}
