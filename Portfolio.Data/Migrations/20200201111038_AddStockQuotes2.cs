using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Data.Migrations
{
    public partial class AddStockQuotes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_Date",
                table: "StockQuotes");

            migrationBuilder.CreateIndex(
                name: "IDX_Date",
                table: "StockQuotes",
                column: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_Date",
                table: "StockQuotes");

            migrationBuilder.CreateIndex(
                name: "IDX_Date",
                table: "StockQuotes",
                column: "Date",
                unique: true);
        }
    }
}
