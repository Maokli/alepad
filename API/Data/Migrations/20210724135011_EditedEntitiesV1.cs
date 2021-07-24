using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class EditedEntitiesV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatRooms_ChatRoomId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_IdentityUser_SenderId1",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatRoomId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderId1",
                table: "Messages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderId1",
                table: "Messages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatRoomId",
                table: "Messages",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId1",
                table: "Messages",
                column: "SenderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatRooms_ChatRoomId",
                table: "Messages",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_IdentityUser_SenderId1",
                table: "Messages",
                column: "SenderId1",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
