using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace housingCooperative.Migrations
{
    public partial class Order_To_Phase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "tblPhaseEntity",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "tblPhaseEntity");
        }
    }
}
