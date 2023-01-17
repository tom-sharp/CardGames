using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PlayerCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Players",
                table: "PlayRounds",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "WinRankId",
                table: "PlayRounds",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Players",
                table: "PlayRounds");

            migrationBuilder.DropColumn(
                name: "WinRankId",
                table: "PlayRounds");
        }
    }
}
