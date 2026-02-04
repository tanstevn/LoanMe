using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adjust_DataSeed_For_BlacklistMobile_BlacklistEmailDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlackListEmailDomains",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "BlackListEmailDomains",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "BlacklistMobiles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "BlacklistMobiles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlackListEmailDomains",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 4, 17, 19, 19, 161, DateTimeKind.Utc).AddTicks(8324));

            migrationBuilder.UpdateData(
                table: "BlackListEmailDomains",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 4, 17, 19, 19, 161, DateTimeKind.Utc).AddTicks(8324));

            migrationBuilder.UpdateData(
                table: "BlacklistMobiles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 4, 17, 19, 19, 164, DateTimeKind.Utc).AddTicks(8812));

            migrationBuilder.UpdateData(
                table: "BlacklistMobiles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 4, 17, 19, 19, 164, DateTimeKind.Utc).AddTicks(8812));
        }
    }
}
