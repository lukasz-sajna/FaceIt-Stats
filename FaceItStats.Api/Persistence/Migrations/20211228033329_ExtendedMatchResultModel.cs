using Microsoft.EntityFrameworkCore.Migrations;

namespace FaceItStats.Api.Persistence.Migrations
{
    public partial class ExtendedMatchResultModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContestId",
                table: "MatchResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstOptionId",
                table: "MatchResult",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondOptionId",
                table: "MatchResult",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BetsSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetsSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetsSettings");

            migrationBuilder.DropColumn(
                name: "ContestId",
                table: "MatchResult");

            migrationBuilder.DropColumn(
                name: "FirstOptionId",
                table: "MatchResult");

            migrationBuilder.DropColumn(
                name: "SecondOptionId",
                table: "MatchResult");
        }
    }
}
