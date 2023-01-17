using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class SeparatePlayerAndRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayRounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Card1Signature = table.Column<byte>(type: "tinyint", nullable: false),
                    Card2Signature = table.Column<byte>(type: "tinyint", nullable: false),
                    Card3Signature = table.Column<byte>(type: "tinyint", nullable: false),
                    Card4Signature = table.Column<byte>(type: "tinyint", nullable: false),
                    Card5Signature = table.Column<byte>(type: "tinyint", nullable: false),
                    Card3RankId = table.Column<byte>(type: "tinyint", nullable: false),
                    Card4RankId = table.Column<byte>(type: "tinyint", nullable: false),
                    Card5RankId = table.Column<byte>(type: "tinyint", nullable: false),
                    Card5RankName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayRounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayRoundHands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WinRound = table.Column<bool>(type: "bit", nullable: false),
                    Card1Signature = table.Column<byte>(type: "tinyint", nullable: false),
                    Card2Signature = table.Column<byte>(type: "tinyint", nullable: false),
                    Card2RankId = table.Column<byte>(type: "tinyint", nullable: false),
                    Card5RankId = table.Column<byte>(type: "tinyint", nullable: false),
                    Card6RankId = table.Column<byte>(type: "tinyint", nullable: false),
                    HandRankId = table.Column<byte>(type: "tinyint", nullable: false),
                    HandRankName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PlayRoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayRoundHands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayRoundHands_PlayRounds_PlayRoundId",
                        column: x => x.PlayRoundId,
                        principalTable: "PlayRounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayRoundHands_PlayRoundId",
                table: "PlayRoundHands",
                column: "PlayRoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayRoundHands");

            migrationBuilder.DropTable(
                name: "PlayRounds");
        }
    }
}
