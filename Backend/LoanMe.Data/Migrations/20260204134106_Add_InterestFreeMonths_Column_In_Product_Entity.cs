using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_InterestFreeMonths_Column_In_Product_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "InterestFreeMonths",
                table: "Products",
                type: "smallint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Description", "InterestFreeMonths" },
                values: new object[] { "This product is interest-free loan.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InterestFreeMonths",
                value: (short)2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InterestFreeMonths",
                value: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestFreeMonths",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Description",
                value: "This product is interest-free free loan.");
        }
    }
}
