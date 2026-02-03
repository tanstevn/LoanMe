using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanMe.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Product_Entity_With_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    LoanTermMinimum = table.Column<short>(type: "smallint", nullable: false),
                    LoanTermMaximum = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "LoanTermMaximum", "LoanTermMinimum", "Name" },
                values: new object[,]
                {
                    { 1L, "This product provides loan that is interest-free.", (short)300, (short)1, "House" },
                    { 2L, "This product gives the first 2 months interest-free but the loan term duration is 6 months minimum.", (short)60, (short)6, "Car" },
                    { 3L, "This product provides no interest-free.", (short)3, (short)1, "Phone" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
