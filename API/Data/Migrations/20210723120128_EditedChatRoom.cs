using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class EditedChatRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ChatRooms",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "ChatRooms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
