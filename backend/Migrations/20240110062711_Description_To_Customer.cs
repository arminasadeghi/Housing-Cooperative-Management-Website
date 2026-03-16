using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace housingCooperative.Migrations
{
    public partial class Description_To_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblCustomerEntity",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblCustomerEntity");
        }
    }
}
