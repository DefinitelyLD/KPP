using Microsoft.EntityFrameworkCore.Migrations;

namespace Messenger.DAL.Migrations
{
    public partial class UserAccountSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLeft",
                table: "UserAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLeft",
                table: "UserAccounts");
        }
    }
}
