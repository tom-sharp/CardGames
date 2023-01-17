using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Added_RankNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "RankIdCommonCards",
                table: "TexasHands",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "RankIdPrivateCards",
                table: "TexasHands",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "RankName",
                table: "TexasHands",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RankNameCommon",
                table: "TexasHands",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RankNamePrivate",
                table: "TexasHands",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RankIdCommonCards",
                table: "TexasHands");

            migrationBuilder.DropColumn(
                name: "RankIdPrivateCards",
                table: "TexasHands");

            migrationBuilder.DropColumn(
                name: "RankName",
                table: "TexasHands");

            migrationBuilder.DropColumn(
                name: "RankNameCommon",
                table: "TexasHands");

            migrationBuilder.DropColumn(
                name: "RankNamePrivate",
                table: "TexasHands");
        }
    }
}
