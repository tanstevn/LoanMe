using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_ActiveLoan_Product_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActiveLoans_ProductId",
                table: "ActiveLoans");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveLoans_ProductId",
                table: "ActiveLoans",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActiveLoans_ProductId",
                table: "ActiveLoans");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveLoans_ProductId",
                table: "ActiveLoans",
                column: "ProductId",
                unique: true);
        }
    }
}
