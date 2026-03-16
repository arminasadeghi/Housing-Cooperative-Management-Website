using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace housingCooperative.Migrations
{
    public partial class Name_To_Plot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblContractEntity_PlotId",
                table: "tblContractEntity");

            migrationBuilder.AddColumn<string>(
                name: "ContractId",
                table: "tblPlotEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tblPlotEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tblContractEntity_PlotId",
                table: "tblContractEntity",
                column: "PlotId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblContractEntity_PlotId",
                table: "tblContractEntity");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "tblPlotEntity");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "tblPlotEntity");

            migrationBuilder.CreateIndex(
                name: "IX_tblContractEntity_PlotId",
                table: "tblContractEntity",
                column: "PlotId");
        }
    }
}
