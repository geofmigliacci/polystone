using Microsoft.EntityFrameworkCore.Migrations;

namespace Polystone.Business.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    HashedName = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    TotalXp = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalStardust = table.Column<int>(type: "INTEGER", nullable: false),
                    CaughtPokemons = table.Column<int>(type: "INTEGER", nullable: false),
                    EscapedPokemons = table.Column<int>(type: "INTEGER", nullable: false),
                    ShinyPokemons = table.Column<int>(type: "INTEGER", nullable: false),
                    Pokestops = table.Column<int>(type: "INTEGER", nullable: false),
                    Rockets = table.Column<int>(type: "INTEGER", nullable: false),
                    Raids = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
