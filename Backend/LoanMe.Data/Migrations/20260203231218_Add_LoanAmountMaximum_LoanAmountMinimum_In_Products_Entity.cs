using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_LoanAmountMaximum_LoanAmountMinimum_In_Products_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LoanAmountMaximum",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LoanAmountMinimum",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LoanAmountMaximum", "LoanAmountMinimum" },
                values: new object[] { 2000000m, 10000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "LoanAmountMaximum", "LoanAmountMinimum" },
                values: new object[] { 20000m, 1000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "LoanAmountMaximum", "LoanAmountMinimum" },
                values: new object[] { 2000m, 100m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanAmountMaximum",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LoanAmountMinimum",
                table: "Products");
        }
    }
}
