using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_InterestRate_And_EstablishmentFee_Columns_In_Product_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstablishmentFee",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InterestRate",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Description", "EstablishmentFee", "InterestRate" },
                values: new object[] { "This product is interest-free free loan.", 300m, 0m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "EstablishmentFee", "InterestRate" },
                values: new object[] { 250m, 0.05m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "EstablishmentFee", "InterestRate" },
                values: new object[] { 100m, 0.02m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstablishmentFee",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Description",
                value: "This product provides loan that is interest-free.");
        }
    }
}
