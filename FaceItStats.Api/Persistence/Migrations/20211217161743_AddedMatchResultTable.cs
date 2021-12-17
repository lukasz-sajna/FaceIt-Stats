using Microsoft.EntityFrameworkCore.Migrations;

namespace FaceItStats.Api.Persistence.Migrations
{
    public partial class AddedMatchResultTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchId = table.Column<string>(nullable: false),
                    IsWin = table.Column<bool>(nullable: false),
                    Kills = table.Column<int>(nullable: false),
                    KdRatio = table.Column<decimal>(nullable: false),
                    IsStarted = table.Column<bool>(nullable: false),
                    IsCancelled = table.Column<bool>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    IsResultSent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchResult", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchResult");
        }
    }
}
