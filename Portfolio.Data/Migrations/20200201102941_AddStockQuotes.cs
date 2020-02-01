using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Data.Migrations
{
    public partial class AddStockQuotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockQuotes",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Open = table.Column<decimal>(nullable: true),
                    High = table.Column<decimal>(nullable: true),
                    Low = table.Column<decimal>(nullable: true),
                    Close = table.Column<decimal>(nullable: false),
                    Volume = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockQuotes", x => new { x.Code, x.Date });
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Date",
                table: "StockQuotes",
                column: "Date",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockQuotes");
        }
    }
}
