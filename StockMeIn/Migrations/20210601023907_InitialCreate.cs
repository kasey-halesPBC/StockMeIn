using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMeIn.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VIN = table.Column<string>(nullable: true),
                    typeNU = table.Column<string>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    make = table.Column<string>(nullable: true),
                    model = table.Column<string>(nullable: true),
                    body = table.Column<string>(nullable: true),
                    color = table.Column<string>(nullable: true),
                    inventoryDate = table.Column<DateTime>(nullable: false),
                    cost = table.Column<decimal>(nullable: false),
                    salePrice = table.Column<decimal>(nullable: false),
                    status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
