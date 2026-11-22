using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Class = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Hitpoints = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "AdminNote", "Class", "CreatedAt", "Hitpoints", "Intelligence", "Level", "Name", "Strength" },
                values: new object[,]
                {
                    { 1, "playtest account", "Warrior", new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 200, 4, 5, "Brynn", 18 },
                    { 2, "top of the leaderboard", "Mage", new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 140, 19, 7, "Zeph", 5 },
                    { 3, null, "Rogue", new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 120, 8, 3, "Nyx", 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
