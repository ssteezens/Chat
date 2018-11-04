using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class CleanupMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatRoomId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChatRoomId",
                table: "ChatEntrys",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatRoomId",
                table: "Users",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatEntrys_ChatRoomId",
                table: "ChatEntrys",
                column: "ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatEntrys_ChatRooms_ChatRoomId",
                table: "ChatEntrys",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ChatRooms_ChatRoomId",
                table: "Users",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatEntrys_ChatRooms_ChatRoomId",
                table: "ChatEntrys");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ChatRooms_ChatRoomId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatRoomId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ChatEntrys_ChatRoomId",
                table: "ChatEntrys");

            migrationBuilder.DropColumn(
                name: "ChatRoomId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatRoomId",
                table: "ChatEntrys");
        }
    }
}
