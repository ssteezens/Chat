using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Refactor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntrys_ChatRooms_ChatRoomId",
                table: "ChatEntrys");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntrys_Users_UserId",
                table: "ChatEntrys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatEntrys",
                table: "ChatEntrys");

            migrationBuilder.RenameTable(
                name: "ChatEntrys",
                newName: "ChatMessages");

            migrationBuilder.RenameIndex(
                name: "IX_ChatEntrys_UserId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatEntrys_ChatRoomId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_ChatRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                table: "ChatMessages",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_UserId",
                table: "ChatMessages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_UserId",
                table: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "ChatMessages",
                newName: "ChatEntrys");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_UserId",
                table: "ChatEntrys",
                newName: "IX_ChatEntrys_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_ChatRoomId",
                table: "ChatEntrys",
                newName: "IX_ChatEntrys_ChatRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatEntrys",
                table: "ChatEntrys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntrys_ChatRooms_ChatRoomId",
                table: "ChatEntrys",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntrys_Users_UserId",
                table: "ChatEntrys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
