using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TexasHands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Win = table.Column<bool>(type: "bit", nullable: false),
                    Players = table.Column<byte>(type: "tinyint", nullable: false),
                    Rank = table.Column<byte>(type: "tinyint", nullable: false),
                    PrivateCard1 = table.Column<byte>(type: "tinyint", nullable: false),
                    PrivateCard2 = table.Column<byte>(type: "tinyint", nullable: false),
                    CommonCard1 = table.Column<byte>(type: "tinyint", nullable: false),
                    CommonCard2 = table.Column<byte>(type: "tinyint", nullable: false),
                    CommonCard3 = table.Column<byte>(type: "tinyint", nullable: false),
                    CommonCard4 = table.Column<byte>(type: "tinyint", nullable: false),
                    CommonCard5 = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TexasHands", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TexasHands");
        }
    }
}
