using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Polystone.Business.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountCandy",
                columns: table => new
                {
                    Specie = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SmallCandy = table.Column<int>(type: "INTEGER", nullable: false),
                    XLCandy = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountId = table.Column<ulong>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCandy", x => x.Specie);
                });

            migrationBuilder.CreateTable(
                name: "AccountCatch",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Specie = table.Column<int>(type: "INTEGER", nullable: false),
                    CatchDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Cp = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false),
                    Stardust = table.Column<int>(type: "INTEGER", nullable: false),
                    IsShiny = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsShadow = table.Column<bool>(type: "INTEGER", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    AccountId = table.Column<ulong>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCatch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountHistory",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<long>(type: "INTEGER", nullable: false),
                    Pokecoin = table.Column<int>(type: "INTEGER", nullable: false),
                    Stardust = table.Column<int>(type: "INTEGER", nullable: false),
                    PokemonCaught = table.Column<int>(type: "INTEGER", nullable: false),
                    PokestopSpinned = table.Column<int>(type: "INTEGER", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    AccountId = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CurrentHistoryId = table.Column<ulong>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountHistory_CurrentHistoryId",
                        column: x => x.CurrentHistoryId,
                        principalTable: "AccountHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CurrentHistoryId",
                table: "Account",
                column: "CurrentHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Name",
                table: "Account",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountCandy_AccountId",
                table: "AccountCandy",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCatch_AccountId",
                table: "AccountCatch",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountHistory_AccountId",
                table: "AccountHistory",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountCandy_Account_AccountId",
                table: "AccountCandy",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountCatch_Account_AccountId",
                table: "AccountCatch",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountHistory_Account_AccountId",
                table: "AccountHistory",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_AccountHistory_CurrentHistoryId",
                table: "Account");

            migrationBuilder.DropTable(
                name: "AccountCandy");

            migrationBuilder.DropTable(
                name: "AccountCatch");

            migrationBuilder.DropTable(
                name: "AccountHistory");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
