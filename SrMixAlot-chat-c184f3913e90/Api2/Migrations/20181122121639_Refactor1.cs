using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Refactor1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntrys_ChatRooms_ChatRoomId",
                table: "ChatEntrys");

            migrationBuilder.AlterColumn<int>(
                name: "ChatRoomId",
                table: "ChatEntrys",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntrys_ChatRooms_ChatRoomId",
                table: "ChatEntrys",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntrys_ChatRooms_ChatRoomId",
                table: "ChatEntrys");

            migrationBuilder.AlterColumn<int>(
                name: "ChatRoomId",
                table: "ChatEntrys",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntrys_ChatRooms_ChatRoomId",
                table: "ChatEntrys",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
