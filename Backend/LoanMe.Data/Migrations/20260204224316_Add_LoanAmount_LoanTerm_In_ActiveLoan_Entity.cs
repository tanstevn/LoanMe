using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_LoanAmount_LoanTerm_In_ActiveLoan_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LoanAmount",
                table: "ActiveLoans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<short>(
                name: "LoanTerm",
                table: "ActiveLoans",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanAmount",
                table: "ActiveLoans");

            migrationBuilder.DropColumn(
                name: "LoanTerm",
                table: "ActiveLoans");
        }
    }
}
