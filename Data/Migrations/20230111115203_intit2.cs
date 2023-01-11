using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class intit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rank",
                table: "TexasHands",
                newName: "RankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RankId",
                table: "TexasHands",
                newName: "Rank");
        }
    }
}
