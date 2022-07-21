using Microsoft.EntityFrameworkCore.Migrations;

namespace Messenger.DAL.Migrations
{
    public partial class AdminsRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdminsRoom",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdminsRoom",
                table: "Chats");
        }
    }
}
