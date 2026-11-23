using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstimateHours = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResovledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InternalNote = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "Description", "EstimateHours", "InternalNote", "Priority", "ResovledAt", "Status", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Paper jams every few pages.", 2, "Reported by facilities", "Medium", null, "Open", "Printer jams on level 3" },
                    { 2, new DateTime(2026, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Connection dies after about 60 minutes.", 6, null, "High", null, "InProgress", "VPN drops every hour" },
                    { 3, new DateTime(2026, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Password reset email never arrives.", 3, "Check mail relay first", "Urgent", null, "Open", "Cannot log in to payroll" },
                    { 4, new DateTime(2026, 10, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Drivers are two versions behind.", 1, null, "Low", null, "Resolved", "Update desk monitor drivers" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
