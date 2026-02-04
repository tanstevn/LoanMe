using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_BlacklistMobile_BlacklistEmailDomain_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlackListEmailDomains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Domain = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackListEmailDomains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlacklistMobiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlacklistMobiles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BlackListEmailDomains",
                columns: new[] { "Id", "CreatedAt", "Domain" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 2, 4, 17, 19, 19, 161, DateTimeKind.Utc).AddTicks(8324), "hacker.com" },
                    { 2L, new DateTime(2026, 2, 4, 17, 19, 19, 161, DateTimeKind.Utc).AddTicks(8324), "helloworld.com" }
                });

            migrationBuilder.InsertData(
                table: "BlacklistMobiles",
                columns: new[] { "Id", "CreatedAt", "MobileNumber" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 2, 4, 17, 19, 19, 164, DateTimeKind.Utc).AddTicks(8812), "1234567890" },
                    { 2L, new DateTime(2026, 2, 4, 17, 19, 19, 164, DateTimeKind.Utc).AddTicks(8812), "0987654321" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlackListEmailDomains_Domain",
                table: "BlackListEmailDomains",
                column: "Domain");

            migrationBuilder.CreateIndex(
                name: "IX_BlacklistMobiles_MobileNumber",
                table: "BlacklistMobiles",
                column: "MobileNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackListEmailDomains");

            migrationBuilder.DropTable(
                name: "BlacklistMobiles");
        }
    }
}
